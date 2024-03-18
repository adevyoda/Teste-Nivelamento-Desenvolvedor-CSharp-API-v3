namespace Questao5.Application.Commands.Requests
{
    public class AccountMovementRequest
    {
        public string AccountId { get; set; }
        public decimal Amount { get; set; }
        public char MovementType { get; set; }
    }
}

 