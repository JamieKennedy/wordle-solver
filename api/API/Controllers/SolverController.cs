using Common.Contracts;
using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolverController : ControllerBase
    {
        private readonly IGameData _gameData;

        public SolverController(IGameData gameData)
        {
            _gameData = gameData;
        }

        [HttpPost]
        public IActionResult Post([FromBody] GameState? gameState = default)
        {
            return Ok(Solver.Solver.Solve(_gameData, gameState));
        }
    }
}