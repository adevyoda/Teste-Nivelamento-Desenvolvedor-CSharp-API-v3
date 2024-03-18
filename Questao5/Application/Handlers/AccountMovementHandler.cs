using Application.Commands.Responses;

namespace Questao5.Application.Handlers
{
    public class AccountMovementHandler
    {
        public async Task<AccountMovementResponse> Handle(AccountMovementRequest request)
        {
            // Implemente a lógica de processamento da movimentação da conta corrente aqui
            // Verifique as validações de negócio, registre a transação no banco de dados, etc.

            // Retorne o ID da movimentação
            return new AccountMovementResponse { MovementId = "123456" };
        }
    }
}
