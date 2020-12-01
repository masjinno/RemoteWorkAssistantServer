using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemoteWorkAssisntantServer.Constants;
using RemoteWorkAssisntantServer.Models;

namespace RemoteWorkAssisntantServer.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;
        }

        // PUT: api/v1/user/delete
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("delete")]
        public async Task<IActionResult> DeleteUser(UserInfo userInfo)
        {
            var userData = await _context.UserInfos.FindAsync(userInfo.MailAddress);
            _context.Entry(userInfo).State = EntityState.Deleted;

            try
            {
                _context.Remove(userInfo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserInfoExists(userInfo.MailAddress))
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

        // POST: api/v1/user
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserInfo>> PostUser(UserInfo userInfo)
        {
            if (UserInfoExists(userInfo.MailAddress))
            {
                return Conflict(new Error(Messages.EMAIL_CONFLICT));
            }

            _context.UserInfos.Add(userInfo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserInfo", new { mailAddress = userInfo.MailAddress }, userInfo);
        }

        private bool UserInfoExists(string mailAddress)
        {
            return _context.UserInfos.Any(e => e.MailAddress.Equals(mailAddress));
        }
    }
}
