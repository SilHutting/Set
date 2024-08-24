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
        public async Task<IEnumerable<Game>> GetGame()
        {
            var games = await _gameRepo.GetAllGames();
            //var games = await _context.Game.ToListAsync();
            return await _gameRepo.GetAllGames();
        }

        // GET: api/game/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(long id)
        {
            var game = await _context.Game
                                    .Include(g => g.TableCards)
                                    .Include(g => g.Deck)
                                    .ThenInclude(d => d.Cards)
                                    .FirstOrDefaultAsync(g => g.Id == id);  
            
            if (game == null)
            {
                return NotFound();
            }
            game.TableCards = await _context.Card.Where(c => c.GameId == id && c.Id != 0).ToListAsync();
            game.Deck.Cards = game.Deck.Cards.Where(card => card.Id != 0).ToList();

            return game;
        }

        // Patch: api/Game/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchGame(long id, GameCreateDto gameCreateDto)
        {
            // Using GameCreateDto instead of Game
            var game = await _context.Game.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            // Map the GameCreateDto to a Game object. User can change game name
            _mapper.Map(gameCreateDto, game);
            
            // Save the changes to the database
            await _context.SaveChangesAsync();
            return NoContent();

        }

        // POST: api/Game
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GameReadDto>> PostGame(GameCreateDto gameCreateDto)
        {
            var newGame = new Game();
            // Map the GameCreateDto to a Game object
            _mapper.Map(gameCreateDto, newGame);

            // Add the new game to the database
            _gameRepo.CreateGame(newGame);
            
            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Map the Game object to a GameReadDto
            var GameReadDto = _mapper.Map<GameReadDto>(newGame);
            // Return the GameReadDto
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

    }
}
