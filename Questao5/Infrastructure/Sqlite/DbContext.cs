using System;
using System.Threading.Tasks;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Entities;
using Microsoft.Data.Sqlite;
using Dapper;

namespace Questao5.Infrastructure.Sqlite
{
    public class DbContext : IDbContext
    {
        private readonly DatabaseConfig _databaseConfig;

        public DbContext(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<Account> FindAsync(string accountId)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            return await connection.QuerySingleOrDefaultAsync<Account>("SELECT * FROM contacorrente WHERE idcontacorrente = @Id", new { Id = accountId });
        }

        public async Task AddAsync(Movement movement)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            await connection.ExecuteAsync("INSERT INTO movimento (idmovimento, idcontacorrente, datamovimento, tipomovimento, valor) VALUES (@Id, @AccountId, @MovementDate, @Type, @Value)", movement);
        }

        public async Task<AccountMovementResponse> GetIdempotencyResultAsync(string idempotencyKey)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            return await connection.QueryFirstOrDefaultAsync<AccountMovementResponse>("SELECT * FROM idempotencia WHERE chave_idempotencia = @IdempotencyKey", new { IdempotencyKey = idempotencyKey });
        }

        public async Task SaveIdempotencyResultAsync(string idempotencyKey, AccountMovementResponse response)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            await connection.ExecuteAsync("INSERT INTO idempotencia (chave_idempotencia, requisicao, resultado) VALUES (@IdempotencyKey, @Request, @Result)", new { IdempotencyKey = idempotencyKey, Request = "YourRequestDataHere", Result = "YourResultDataHere" });
        }

        public async Task<Account> GetAccountByIdAsync(string accountId)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            return await connection.QueryFirstOrDefaultAsync<Account>("SELECT * FROM contacorrente WHERE idcontacorrente = @AccountId", new { AccountId = accountId });
        }

        public async Task AddMovementAsync(Movement movement)
        {
            // Verifica se a conta associada à movimentação existe e está ativa
            var account = await GetAccountByIdAsync(movement.AccountId);
            if (account == null)
            {
                throw new ArgumentException("Conta não encontrada", nameof(movement.AccountId));
            }

            if (!account.ativo)
            {
                throw new InvalidOperationException("A conta não está ativa");
            }

            // Adiciona a nova movimentação ao banco de dados
            await AddAsync(movement);
        }

        public async Task<IEnumerable<Movement>> GetMovementsByAccountIdAsync(string accountId)
        {
            using var connection = new SqliteConnection(_databaseConfig.Name);
            return await connection.QueryAsync<Movement>("SELECT * FROM movimento WHERE idcontacorrente = @AccountId", new { AccountId = accountId });
        }

    }
}
