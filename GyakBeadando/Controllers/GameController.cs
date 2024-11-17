using GyakBeadando.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using GyakBeadando.Models;


namespace GyakBeadando.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {

        private readonly GyakbeaContext _context;

        public GameController(GyakbeaContext context)
        {
            _context = context;
        }

        [HttpGet("games")]
        public IActionResult GetAllGames()
        {
            var games = _context.Games.OrderBy(g => g.Id);

            if(games == null)
            {
                return NotFound();
            } else
            {
                return Ok(games);
            }
        }

        [HttpGet("games/{id}")]
        public IActionResult GetGameById(int id)
        {
            var game = _context.Games.FirstOrDefault(x => x.Id == id);

            if (game == null) {
                return NotFound();
            } else
            {
                return Ok(game);
            }
        }

        [HttpPost("games/add-game")]
        public async Task<IActionResult> AddGame([FromBody] Game newGame)
        {
            if (newGame == null) {
                return BadRequest("Game is null.");
            }

            await _context.Games.AddAsync(newGame);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGameById), new {id = newGame.Id}, newGame);
        }

        [HttpDelete("games/delete-game/{id}")]
        public async Task<IActionResult> DeleteGameById(int id)
        {
            // It MUST not delete the dummy data
            if(id <= 16)
            {
                return BadRequest("Id must be greater than 16.");
            }

            var game = await _context.Games.FindAsync(id);

            if(game == null)
            {
                return NotFound("Game not found.");
            }

            _context.Games.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("games/isplayed/{id}")]
        public async Task<IActionResult> ChangeIsPlayedById(int id, [FromBody] bool isPlayed)
        {
            var game = await _context.Games.FindAsync(id);

            if (game == null) return NotFound();

            game.Isplayed = isPlayed;

            _context.Entry(game).Property(e => e.Isplayed).IsModified = true;
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
