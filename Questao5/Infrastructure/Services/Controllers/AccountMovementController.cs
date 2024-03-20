// AccountMovementController.cs
using Microsoft.AspNetCore.Mvc;
using Questao5.Application.Commands.Requests;
using Questao5.Application.Commands.Responses;
using Questao5.Application.Services;
using System;
using System.Threading.Tasks;

namespace Questao5.Infrastructure.Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountMovementController : ControllerBase
    {
        private readonly IAccountMovementService _accountMovementService;

        public AccountMovementController(IAccountMovementService accountMovementService)
        {
            _accountMovementService = accountMovementService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(AccountMovementRequest request)
        {
            try
            {
                var movementId = await _accountMovementService.MoveAccountAsync(request);
                return Ok(new { MovementId = movementId });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message, ErrorType = "BAD_REQUEST" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { ErrorMessage = ex.Message, ErrorType = "BAD_REQUEST" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message, ErrorType = "INTERNAL_SERVER_ERROR" });
            }
        }
    }
}
