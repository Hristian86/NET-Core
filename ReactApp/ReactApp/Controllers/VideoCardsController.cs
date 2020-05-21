using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactApp.Data;
using ReactApp.Data.DBModels;

namespace ReactApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoCardsController : ControllerBase
    {
        private readonly ApplicationDbContext db;

        public VideoCardsController(ApplicationDbContext db)
        {
            this.db = db;
        }

        // GET: api/VideoCards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VideoCards>>> GetVideoCards()
        {
            return await db.VideoCards.ToListAsync();
        }

        // GET: api/VideoCards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VideoCards>> GetVideoCards(int id)
        {
            var videoCards = await db.VideoCards.FindAsync(id);

            if (videoCards == null)
            {
                return NotFound();
            }

            return videoCards;
        }

        // PUT: api/VideoCards/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideoCards(int id, VideoCards videoCards)
        {
            if (id != videoCards.Id)
            {
                return BadRequest();
            }

            db.Entry(videoCards).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoCardsExists(id))
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

        // POST: api/VideoCards
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<VideoCards>> PostVideoCards(VideoCards videoCards)
        {
            db.VideoCards.Add(videoCards);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetVideoCards", new { id = videoCards.Id }, videoCards);
        }

        // DELETE: api/VideoCards/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<VideoCards>> DeleteVideoCards(int id)
        {
            var videoCards = await db.VideoCards.FindAsync(id);
            if (videoCards == null)
            {
                return NotFound();
            }

            db.VideoCards.Remove(videoCards);
            await db.SaveChangesAsync();

            return videoCards;
        }

        private bool VideoCardsExists(int id)
        {
            return db.VideoCards.Any(e => e.Id == id);
        }
    }
}