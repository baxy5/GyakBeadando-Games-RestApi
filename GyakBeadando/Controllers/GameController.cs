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

        // get all games
        [HttpGet("games")]
        public IActionResult GetGames()
        {
            var games = _context.Games;

            if(games == null)
            {
                return NotFound();
            } else
            {
                return Ok(games);
            }
        }

        // get game by id
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

        // add new game
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

        // delete game by id
        [HttpDelete("games/delete-game/{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
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

    }
}
