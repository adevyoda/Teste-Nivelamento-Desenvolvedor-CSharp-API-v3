using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Requests
{
    public class AccountMovementRequest
    {
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public MovementType MovementType { get; set; }

        public AccountMovementRequest() 
        { 
            AccountId=string.Empty;
        }
    }
}
