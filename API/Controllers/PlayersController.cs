using NoviCode.API.DTO;
using Microsoft.AspNetCore.Mvc;
using NoviCode.Application.Service;
using NoviCode.Domain.Entity;

namespace NoviCode.API.Controllers
{
    [ApiController]
    [Route("players")]
    public class PlayersController:ControllerBase
    {
        private readonly IPlayerService _player;

        public PlayersController(IPlayerService player) => _player = player;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlayerRequest request, CancellationToken cancellationToken)
        {
            Player player;
            try
            {
                player = await _player.CreateAsync(request.Name, request.Score, cancellationToken);
            }
            catch(ArgumentException ex)
            { return BadRequest(new { error = ex.Message }); }

            return CreatedAtAction(nameof(GetById), new { id = player.Id }, PlayerResponse.From(player));
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var player = await _player.GetByIdAsync(id, cancellationToken);
            return player is null ? NotFound() : Ok(PlayerResponse.From(player));
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var players = await _player.GetAllAsync(cancellationToken);
            return Ok(players.Select(PlayerResponse.From));
        }
    }
}
