// IAccountBalanceService.cs em app\services

using System.Threading.Tasks;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Services
{
    public interface IAccountBalanceService
    {
        Task<AccountBalanceResponse> GetAccountBalanceAsync(string accountId);
    }
}
