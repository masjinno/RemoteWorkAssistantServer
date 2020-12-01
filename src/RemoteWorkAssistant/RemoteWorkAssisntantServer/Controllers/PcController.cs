using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemoteWorkAssisntantServer.Models;

namespace RemoteWorkAssisntantServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PcController : ControllerBase
    {
        private readonly PcContext _context;

        public PcController(PcContext context)
        {
            _context = context;
        }

        // GET: api/Pc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PcInfo>>> GetPcInfos()
        {
            return await _context.PcInfos.ToListAsync();
        }

        // GET: api/Pc/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PcInfo>> GetPcInfo(long id)
        {
            var pcInfo = await _context.PcInfos.FindAsync(id);

            if (pcInfo == null)
            {
                return NotFound();
            }

            return pcInfo;
        }

        // PUT: api/Pc/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPcInfo(long id, PcInfo pcInfo)
        {
            if (id != pcInfo.Id)
            {
                return BadRequest();
            }

            _context.Entry(pcInfo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PcInfoExists(id))
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

        // POST: api/Pc
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PcInfo>> PostPcInfo(PcInfo pcInfo)
        {
            _context.PcInfos.Add(pcInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPcInfo", new { id = pcInfo.Id }, pcInfo);
        }

        // DELETE: api/Pc/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePcInfo(long id)
        {
            var pcInfo = await _context.PcInfos.FindAsync(id);
            if (pcInfo == null)
            {
                return NotFound();
            }

            _context.PcInfos.Remove(pcInfo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PcInfoExists(long id)
        {
            return _context.PcInfos.Any(e => e.Id == id);
        }
    }
}
