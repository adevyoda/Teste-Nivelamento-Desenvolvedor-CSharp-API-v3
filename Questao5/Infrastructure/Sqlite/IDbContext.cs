// IDbContext.cs

using System.Collections.Generic;
using System.Threading.Tasks;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;

namespace Questao5.Infrastructure.Sqlite
{
    public interface IDbContext
    {
        Task<Account> FindAsync(string accountId);
        Task AddAsync(Movement movement);
        Task<AccountMovementResponse> GetIdempotencyResultAsync(string idempotencyKey);
        Task SaveIdempotencyResultAsync(string idempotencyKey, AccountMovementResponse response);
        Task<Account> GetAccountByIdAsync(string accountId);
        Task<IEnumerable<Movement>> GetMovementsByAccountIdAsync(string accountId);
        Task AddMovementAsync(Movement movement); // Adicione este método à sua interface IDbContext
    
}
}
