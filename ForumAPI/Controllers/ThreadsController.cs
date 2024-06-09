using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForumAPI.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ForumAPI.Data;
using Microsoft.AspNetCore.Cors;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ForumAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ThreadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Thread>>> GetThreads()
        {
            return await _context.Threads.Include(t => t.Posts).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Models.Thread>>> GetThread(string id)
        {
            var thread = await _context.Threads.Where(t => t.Title.Contains(id) || t.Author.Contains(id)).Include(t => t.Category).ToListAsync();

            if (thread == null)
            {
                return NotFound();
            }

            return thread;
        }

        [HttpPost]
        public async Task<ActionResult<Models.Thread>> CreateThread(Models.Thread thread)
        {
            _context.Threads.Add(thread);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetThread), new { id = thread.Id }, thread);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateThread(int id, Models.Thread thread)
        {
            if (id != thread.Id)
            {
                return BadRequest();
            }

            _context.Entry(thread).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ThreadExists(id))
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

        private bool ThreadExists(int id)
        {
            return _context.Threads.Any(e => e.Id == id);
        }
    }
}