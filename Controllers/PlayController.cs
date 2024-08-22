// All Set game player actions are handled by the PlayController. The PlayController is responsible for handling all game actions, including:
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
using Set.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Set.Profiles;

namespace Set.Controllers
{
    [Route("api/Play")]
    [ApiController]
    public class PlayController : ControllerBase
    {
        private readonly SetContext _context;
        private readonly IPlayService _playService;
        private readonly IMapper _mapper;
        public PlayController(IPlayService playRepo, SetContext context, IMapper mapper)
        {
            _playService = playRepo;
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Play/5/Hint
        [HttpGet("{id}/Hint")]
        public async Task<ActionResult<IEnumerable<Card>>> GetHint(int gameId)
        {
            var hint = _playService.GetHint(gameId);

            if (hint == null)
            {
                return this.StatusCode(StatusCodes.Status200OK, new { Message = "Game is invalid!" });
            }

            return this.StatusCode(StatusCodes.Status200OK, new { Message = "Hint retrieved successfully", Hint = hint });
        }

        // POST: api/Play/5/TrySet
        [HttpPost("{id}/TrySet")]
        public async Task<ActionResult<IEnumerable<Game>>> TrySet(int gameId, SetTryDto setTryDto)
        {
            var result = _playService.TrySet(gameId, setTryDto);

            if (result == null)
            {
                return this.StatusCode(StatusCodes.Status200OK, new { Message = "Game or Set is invalid!" });
            }

            _context.SaveChanges();
            return this.StatusCode(StatusCodes.Status200OK, new { Message = "Set is valid!", Game = result });

        }


    }
}
