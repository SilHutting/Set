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

        // POST: api/Play/5/TrySet
        [HttpPost("{id}")]
        public async Task<ActionResult<IEnumerable<Game>>> TrySet(int gameId, SetTryDto setTryDto)
        {
            var newGameState = _playService.TrySet(gameId, setTryDto);

            if (newGameState == null)
            {
                return this.StatusCode(StatusCodes.Status200OK, new { Message = "Set is invalid!" });
            }

            return this.StatusCode(StatusCodes.Status200OK, new { Message = "Set is valid!", Game = newGameState });

        }


    }
}
