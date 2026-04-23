using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTaskTrackerAPI.Data;
using StudentTaskTrackerAPI.Models;

namespace StudentTaskTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public QuotesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("random")]
        public async Task<ActionResult<Quote>> GetRandomQuote()
        {
            var quotes = await _context.Quotes.ToListAsync();

            if (!quotes.Any())
            {
                return NotFound();
            }

            var random = new Random();
            return quotes[random.Next(quotes.Count)];
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Quote>>> GetQuotes()
        {
            return await _context.Quotes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Quote>> PostQuote(Quote quote)
        {
            _context.Quotes.Add(quote);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetQuotes), new { id = quote.QuoteId }, quote);
        }

        [HttpPost("bulk")]
public async Task<IActionResult> AddQuotesBulk(List<Quote> quotes)
{
    _context.Quotes.AddRange(quotes);
    await _context.SaveChangesAsync();

    return Ok(quotes);
}
    }
}