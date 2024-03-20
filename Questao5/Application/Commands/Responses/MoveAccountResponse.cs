namespace Questao5.Application.Commands.Responses
{
    public class MoveAccountResponse
    {
        public string MovementId { get; set; }
        public MoveAccountResponse() 
        {
            MovementId = string.Empty;
        }
    }
}
