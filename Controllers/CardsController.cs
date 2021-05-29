using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VaiVoa.Data;
using VaiVoa.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VaiVoa.Controllers
{
    public class CardsController : Controller
    {
        private readonly cardcontext _context;

        public CardsController(cardcontext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCard()
        {
            return await _context.Cards.ToListAsync();
        }

        // GET: api/Cartoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCards(int id)
        {
            var card = await _context.Cards.FindAsync(id);

            if (card == null)
            {
                return NotFound();
            }

            return card;
        }

        [HttpGet]
        public async Task<ActionResult<Card>> GetEmail(string email)
        {
            var card = from c in _context.Cards select c;
            if (!String.IsNullOrEmpty(email))
            {
                card = card.Where(c => c.Email.Equals(email)).OrderBy(c => c.Date).Distinct();
            }

            if (card == null)
            {
                return NotFound();
            }

            var user = await card.ToListAsync();
            return CreatedAtAction(nameof(GetEmail), user);
        }

        // PUT: api/Cartoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCard(int id, Card card)
        {
            if (id != card.Id)
            {
                return BadRequest();
            }

            _context.Entry(card).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCard(Card card)
        {
            Card c = new Card();

            card.CardNumber = c.GenerateCard();

            DateTime data = DateTime.Now;
            card.Date = data;

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card.CardNumber);
        }

        // DELETE: api/Cartoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(int? id)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(m => m.Id == id);
            if (card == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
