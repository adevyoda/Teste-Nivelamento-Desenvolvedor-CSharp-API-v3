using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Questao5.Domain.Enumerators;
using Questao5.Infrastructure.Sqlite;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Questao5.Application.Services
{
    public class AccountBalanceService : IAccountBalanceService
    {
        private readonly IDbContext _dbContext;

        public AccountBalanceService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AccountBalanceResponse> GetAccountBalanceAsync(string accountId)
        {
            // Verificar se a conta corrente existe e está ativa
            var account = await _dbContext.GetAccountByIdAsync(accountId);
            if (account == null)
            {
                return new AccountBalanceResponse
                {
                    ErrorType = ErrorType.INVALID_ACCOUNT,
                    ErrorMessage = "Conta corrente não encontrada."
                };
            }
            if (!account.ativo)
            {
                return new AccountBalanceResponse
                {
                    ErrorType = ErrorType.INACTIVE_ACCOUNT,
                    ErrorMessage = "Conta corrente está inativa."
                };
            }

            // Consultar as movimentações da conta com o ID fornecido
            var movements = await _dbContext.GetMovementsByAccountIdAsync(accountId);

            if (movements == null || !movements.Any())
            {
                // Se não houver movimentações, retornar saldo zero
                return new AccountBalanceResponse
                {
                    AccountId = accountId,
                    Balance = 0,
                    ErrorType = ErrorType.NONE // Não há erros
                };
            }

            // Calcular o saldo com base nas movimentações
            decimal creditos = movements.Where(m => m.MovementType == MovementType.CREDIT).Sum(m => m.MovementValue);
            decimal debitos = movements.Where(m => m.MovementType == MovementType.DEBIT).Sum(m => m.MovementValue);
            decimal saldo = creditos - debitos;

            // Retornar a resposta com o saldo calculado
            return new AccountBalanceResponse
            {
                AccountId = accountId,
                Balance = saldo,
                ErrorType = ErrorType.NONE // Não há erros
            };
        }
    }
}
