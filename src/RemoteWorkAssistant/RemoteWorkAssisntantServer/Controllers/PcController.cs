using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemoteWorkAssisntantServer.Constants;
using RemoteWorkAssisntantServer.Dto;
using RemoteWorkAssisntantServer.Models;

namespace RemoteWorkAssisntantServer.Controllers
{
    /// <summary>
    /// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PcController : ControllerBase
    {
        private readonly RemoteWorkAssistantContext _context;

        public PcController(RemoteWorkAssistantContext context)
        {
            _context = context;
        }

        // GET: api/pc
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PcInfo>>> GetPcInfos()
        {
            return await _context.PcInfos.ToListAsync();
        }

        // GET: api/pc/5
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

        // PUT: api/pc/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutPcInfo(PcPutReq pcPutReq)
        {
            if (!this._context.Authenticate(pcPutReq))
            {
                return BadRequest(new Error(Messages.AUTHENTICATION_ERROR));
            }

            PcInfo pcInfo = pcPutReq.ConvertToPcInfo();
            this._context.Entry(pcInfo).State = EntityState.Modified;

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this._context.PcInfoExists(pcInfo.Id))
                {
                    return NotFound(new Error(Messages.PC_NAME_NOT_FOUND));
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// POST: api/pc
        /// </summary>
        /// <param name="pcPostReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostPcInfo(PcPostReq pcPostReq)
        {
            if (!this._context.Authenticate(pcPostReq))
            {
                return BadRequest(new Error(Messages.AUTHENTICATION_ERROR));
            }

            PcInfo pcInfo = pcPostReq.ConvertToPcInfo();
            if (this._context.PcInfoExists(pcInfo.Id))
            {
                return Conflict(new Error(Messages.PC_NAME_CONFLICT));
            }

            this._context.PcInfos.Add(pcInfo);
            await this._context.SaveChangesAsync();

            return Ok();
        }

        //// DELETE: api/pc/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePcInfo(long id)
        //{
        //    var pcInfo = await _context.PcInfos.FindAsync(id);
        //    if (pcInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.PcInfos.Remove(pcInfo);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}
    }
}
