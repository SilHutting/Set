using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Set.Data;
using Set.Models;

namespace Set.Controllers
{
    [Route("api/game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly GameContext _context;
        private readonly IGameRepo _gameRepo;

        public GameController(IGameRepo gameRepo, GameContext context)
        {
            _gameRepo = gameRepo;
            _context = context;
        }

        // POST: api/game
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetCard()
        {
            return await _context.game.ToListAsync();
        }

        // GET: api/Card/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(long id)
        {
            var card = await _context.game.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }
    }
}
