using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Sqlite;
using System;
using System.Threading.Tasks;

namespace Questao5.Application.Handlers
{
    public class AccountMovementHandler
    {
        private readonly IDbContext _dbContext;

        public AccountMovementHandler(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AccountMovementResponse> Handle(AccountMovementRequest request)
        {
            // Verificar se a conta corrente está ativa
            var account = await _dbContext.GetAccountByIdAsync(request.AccountId);
            if (account == null || !account.ativo)
            {
                throw new InvalidOperationException("A conta corrente não está ativa.");
            }

            // Verificar se a movimentação é válida
            if (request.Amount <= 0)
            {
                throw new InvalidOperationException("O valor da movimentação deve ser maior que zero.");
            }

            // Registar a transação no banco de dados
            var movement = new Movement
            {
                AccountId = request.AccountId,
                MovementDate = DateTime.UtcNow,
                MovementType = request.MovementType,
                MovementValue = request.Amount
            };
            await _dbContext.AddMovementAsync(movement);

            // Retornar o ID da movimentação
            return new AccountMovementResponse { MovementId = movement.Id };
        }
    }
}
