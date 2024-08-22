using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Set.Data;
using Set.Models;
using Set.Dtos;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Set.Profiles;

namespace Set.Controllers
{
    [Route("api/Game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly SetContext _context;
        private readonly IGameRepo _gameRepo;
        private readonly IMapper _mapper;
        public GameController(IGameRepo gameRepo, SetContext context, IMapper mapper)
        {
            _gameRepo = gameRepo;
            _context = context;
            _mapper = mapper;
        }

        // Get: api/game
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGame()
        {
            var games = await _gameRepo.GetAllGames();
            //var games = await _context.Game.ToListAsync();

            return this.StatusCode(StatusCodes.Status200OK, new { Message = "Games retrieved successfully", Games = games });
        }

        // GET: api/game/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(long id)
        {
            var game = await _context.Game
                                    //.Include(g => g.TableCards)
                                    .Include(g => g.Deck)
                                    .ThenInclude(d => d.Cards)
                                    .FirstOrDefaultAsync(g => g.Id == id);  
            
            if (game == null)
            {
                return NotFound();
            }
            var cards = await _context.Card
                            .Where(c => c.GameId == id && c.Id != 0)
                            .ToListAsync();
                            game.TableCards = cards;
            game.Deck.Cards = game.Deck.Cards.Where(card => card.Id != 0).ToList();

            return game;
        }

        // PUT: api/Game/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(long id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            _context.Entry(game).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Game
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameReadDto>> PostGame()
        {
            var newGame = new Game();
            _context.Game.Add(newGame);
            await _context.SaveChangesAsync();

            var GameReadDto = _mapper.Map<GameReadDto>(newGame);
            return CreatedAtAction(nameof(GetGame), new { id = newGame.Id }, newGame);
        }

        // DELETE: api/Game/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(long id)
        {
            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            _context.Game.Remove(game);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GameExists(long id)
        {
            return _context.Game.Any(e => e.Id == id);
        }
    }
}
