using System;
using System.Threading.Tasks;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Sqlite;

namespace Questao5.Application.Services
{
    public class AccountMovementService : IAccountMovementService
    {
        private readonly IDbContext _dbContext;

        public AccountMovementService(IDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<AccountMovementResponse> MoveAccountAsync(AccountMovementRequest request)
        {
            // Verificar se a requisição já foi processada anteriormente (Idempotência)
            var idempotencyKey = ComputeIdempotencyKey(request);
            var previousResult = await _dbContext.GetIdempotencyResultAsync(idempotencyKey);
            if (previousResult != null)
                return previousResult;

            // Validar se a conta corrente existe e está ativa
            var account = await _dbContext.GetAccountByIdAsync(request.AccountId);
            if (account == null)
                throw new Exception("Conta corrente não encontrada.");

            if (!account.ativo)
                throw new Exception("Conta corrente está inativa.");

            // Validar se o valor é positivo
            if (request.Amount <= 0)
                throw new Exception("Valor da movimentação deve ser positivo.");

            // Validar se o tipo de movimento é válido
            if (request.MovementType != MovementType.CREDIT && request.MovementType != MovementType.DEBIT)
                throw new Exception("Tipo de movimentação inválido.");

            // Persistir os dados na tabela movimento
            await _dbContext.AddMovementAsync(new Movement
            {
                Id = Guid.NewGuid().ToString(),
                AccountId = request.AccountId,
                MovementDate = DateTime.Now,
                MovementType = request.MovementType,
                MovementValue = request.Amount
            });

            // Armazenar o resultado na tabela de Idempotência
            await _dbContext.SaveIdempotencyResultAsync(idempotencyKey, new AccountMovementResponse { MovementId = Guid.NewGuid().ToString() });

            return new AccountMovementResponse { MovementId = Guid.NewGuid().ToString() };
        }

        private string ComputeIdempotencyKey(AccountMovementRequest request)
        {
            // Aqui você pode implementar a lógica para calcular a chave de idempotência
            // a partir dos dados da requisição que garantirá a identificação única da requisição
            // Exemplo: Concatenar os atributos relevantes da requisição em uma string
            return $"{request.AccountId}-{request.Amount}-{request.MovementType}";
        }
    }
}
