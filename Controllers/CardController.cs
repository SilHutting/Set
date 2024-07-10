using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Set.Models;

namespace Set.Controllers
{
    [Route("api/card")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly CardContext _context;

        public CardController(CardContext context)
        {
            _context = context;
        }

        // GET: api/Card
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCard()
        {
            return await _context.Card.ToListAsync();
        }

        // GET: api/Card/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(long id)
        {
            var card = await _context.Card.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }
    }
}
