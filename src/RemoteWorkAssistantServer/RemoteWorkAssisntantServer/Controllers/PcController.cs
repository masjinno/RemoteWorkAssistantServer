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

        /// <summary>
        /// PUT: api/v1/pc
        /// </summary>
        /// <param name="pcPostReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostPc(PcPostReq pcPostReq)
        {
            if (!this._context.Authenticate(pcPostReq))
            {
                return BadRequest(new Error(Messages.AUTHENTICATION_ERROR));
            }

            PcRecord pcInfo = pcPostReq.ConvertToPcRecord();
            if (this._context.PcRecordExists(pcInfo.Id))
            {
                return Conflict(new Error(Messages.PC_NAME_CONFLICT));
            }

            this._context.PcTable.Add(pcInfo);
            await this._context.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/v1/pc
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutPc(PcPutReq pcPutReq)
        {
            if (!this._context.Authenticate(pcPutReq))
            {
                return BadRequest(new Error(Messages.AUTHENTICATION_ERROR));
            }

            PcRecord pcInfo = pcPutReq.ConvertToPcRecord();
            if (this._context.PcRecordExists(RemoteWorkAssistantContext.GeneratePcInfoId(pcInfo.MailAddress, pcInfo.PcName)))
            {
                return Conflict(new Error(Messages.PC_NAME_CONFLICT));
            }

            this._context.Entry(pcInfo).State = EntityState.Modified;

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this._context.PcRecordExists(pcInfo.Id))
                {
                    return NotFound(new Error(Messages.PC_NAME_NOT_FOUND));
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // PUT: api/v1/pc/ipaddress
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("ipaddress")]
        public async Task<IActionResult> PutIpAddress(PcIpAddressPutReq pcIpAddressPutReq)
        {
            if (!this._context.Authenticate(pcIpAddressPutReq))
            {
                return BadRequest(new Error(Messages.AUTHENTICATION_ERROR));
            }

            PcRecord pcInfo = pcIpAddressPutReq.ConvertToPcRecord();
            this._context.Entry(pcInfo).State = EntityState.Modified;

            try
            {
                await this._context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this._context.PcRecordExists(pcInfo.Id))
                {
                    return NotFound(new Error(Messages.PC_NAME_NOT_FOUND));
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // PUT: api/v1/pc/ipaddress
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("ipaddress/get")]
        public async Task<ActionResult<PcIpAddressGetPutRes>> PutIpAddressGet(UserAuthorization pcIpAddressPutGetReq)
        {
            if (!this._context.Authenticate(pcIpAddressPutGetReq))
            {
                return BadRequest(new Error(Messages.AUTHENTICATION_ERROR));
            }

            PcIpAddressGetPutRes resp = new PcIpAddressGetPutRes
            {
                MailAddress = pcIpAddressPutGetReq.MailAddress,
                PcData = null
            };
            resp.PcData = await this._context.PcTable
                .Where(pr => pr.MailAddress.Equals(pcIpAddressPutGetReq.MailAddress))
                .Select(pr => new PcInfo { PcName = pr.PcName, IpAddress = pr.IpAddress })
                .ToListAsync();

            return Ok(resp);
        }

        // For Debug
        // GET: api/pc/debug
        [HttpGet("debug")]
        public async Task<ActionResult<IEnumerable<PcRecord>>> GetPcInfos()
        {
            return await _context.PcTable.ToListAsync();
        }

        //// GET: api/v1/pc/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<PcInfo>> GetPcInfo(long id)
        //{
        //    var pcInfo = await _context.PcInfos.FindAsync(id);

        //    if (pcInfo == null)
        //    {
        //        return NotFound();
        //    }

        //    return pcInfo;
        //}

        //// DELETE: api/v1/pc/5
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
