// IAccountMovementService.cs em app\services

using System.Threading.Tasks;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;

namespace Questao5.Application.Services
{
    public interface IAccountMovementService
    {
        Task<AccountMovementResponse> MoveAccountAsync(AccountMovementRequest request);
    }
}
