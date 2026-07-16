using NoviCode.API.DTO;
using Microsoft.AspNetCore.Mvc;
using NoviCode.Application.Service;
using NoviCode.Domain.Entity;
using NoviCode.Domain.Exceptions;

namespace NoviCode.API.Controllers
{
    [ApiController]
    [Route("wallets")]
    public class WalletController : ControllerBase
    {
        private readonly IWalletService _wallets;
        public WalletController(IWalletService wallets) { _wallets = wallets; }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateWalletRequest request, CancellationToken cancellationToken)
        {
            var wallet = await _wallets.CreateWalletAsync(request.PlayerId, request.currency, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id = wallet.Id }, WalletResponse.From(wallet));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var wallet = await _wallets.GetByIdAsync(id, cancellationToken);
            return wallet is null ? NotFound() : Ok(WalletResponse.From(wallet));
        }

        [HttpPost("{id:int}/deposit")]
        public async Task<IActionResult> Deposit(int id, [FromForm] DepositRequest request, CancellationToken cancellationToken)
        {
            
            try
            {
                var wallet = await _wallets.DespositAsync(id, request.Amount, cancellationToken);
                return wallet is null ? NotFound() : Ok(WalletResponse.From(wallet));
            }catch(WalletException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
