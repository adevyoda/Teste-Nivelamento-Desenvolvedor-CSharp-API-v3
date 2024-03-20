// AccountBalanceResponse.cs
using Questao5.Domain.Enumerators;

namespace Questao5.Application.Commands.Responses
{
    public class AccountBalanceResponse
    {
        public string AccountId { get; set; }
        public decimal Balance { get; set; }
        public ErrorType ErrorType { get; set; }
        public string ErrorMessage { get; set; }

        public AccountBalanceResponse() 
        { 
            Balance = 0;    
            AccountId = string.Empty;
            ErrorMessage = string.Empty;
        }
    }


}
