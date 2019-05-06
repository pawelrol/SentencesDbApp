using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SentencesDbApp.Data;
using SentencesDbApp.Models;

namespace SentencesDbApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SentencesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SentencesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Sentences
        [HttpGet]
        public IEnumerable<Sentence> GetSentences()
        {
            return _context.Sentences;
        }

        // GET: api/Sentences/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSentence([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sentence = await _context.Sentences.FindAsync(id);

            if (sentence == null)
            {
                return NotFound();
            }

            return Ok(sentence);
        }

        // PUT: api/Sentences/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSentence([FromRoute] int id, [FromBody] Sentence sentence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sentence.SentenceId)
            {
                return BadRequest();
            }

            _context.Entry(sentence).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SentenceExists(id))
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

        // POST: api/Sentences
        [HttpPost]
        public async Task<IActionResult> PostSentence([FromBody] Sentence sentence)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Sentences.Add(sentence);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSentence", new { id = sentence.SentenceId }, sentence);
        }

        // DELETE: api/Sentences/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSentence([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sentence = await _context.Sentences.FindAsync(id);
            if (sentence == null)
            {
                return NotFound();
            }

            _context.Sentences.Remove(sentence);
            await _context.SaveChangesAsync();

            return Ok(sentence);
        }

        private bool SentenceExists(int id)
        {
            return _context.Sentences.Any(e => e.SentenceId == id);
        }
    }
}