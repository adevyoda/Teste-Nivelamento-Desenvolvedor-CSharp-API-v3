// AccountBalanceController.cs
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Services;
using Questao5.Domain.Enumerators;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountBalanceController : ControllerBase
    {
        private readonly IAccountBalanceService _accountBalanceService;

        public AccountBalanceController(IAccountBalanceService accountBalanceService)
        {
            _accountBalanceService = accountBalanceService;
        }

        [HttpGet("{accountId}", Name = "GetAccountBalance")]
        public async Task<ActionResult<AccountBalanceResponse>> Get(string accountId)
        {
            var accountBalanceResponse = await _accountBalanceService.GetAccountBalanceAsync(accountId);

            if (accountBalanceResponse.ErrorType != ErrorType.NONE)
            {
                return BadRequest(new { ErrorMessage = accountBalanceResponse.ErrorMessage, ErrorType = accountBalanceResponse.ErrorType });
            }

            return Ok(accountBalanceResponse);
        }
    }
}
