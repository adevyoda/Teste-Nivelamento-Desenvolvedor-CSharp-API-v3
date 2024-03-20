// AccountMovementResponse.cs
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses
{
    public class AccountMovementResponse
    {
        public string MovementId { get; set; }
        public string ErrorMessage { get; set; }
        public ErrorType ErrorType { get; set; }

        public AccountMovementResponse() 
        { 
            MovementId=string.Empty;
            ErrorMessage=string.Empty;

        }
    }




}
