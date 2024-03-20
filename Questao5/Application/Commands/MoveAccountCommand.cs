using MediatR;
using Questao5.Application.Commands.Responses;
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands
{
    public class MoveAccountCommand : IRequest<AccountMovementResponse>
    {
        public string AccountId { get; }
        public decimal Amount { get; }
        public MovementType MovementType { get; }

        public MoveAccountCommand(string accountId, decimal amount, MovementType movementType)
        {
            AccountId = accountId;
            Amount = amount;
            MovementType = movementType;
        }
    }
}
