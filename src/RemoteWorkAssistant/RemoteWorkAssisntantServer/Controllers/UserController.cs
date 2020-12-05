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
    public class UserController : ControllerBase
    {
        private readonly RemoteWorkAssistantContext _context;

        public UserController(RemoteWorkAssistantContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// ユーザー情報を削除する
        /// PUT: api/v1/user/delete
        /// </summary>
        /// <param name="userReq"></param>
        /// <returns></returns>
        [HttpPut("delete")]
        public async Task<IActionResult> DeleteUser(UserReq userReq)
        {
            if (!this._context.Authenticate(userReq))
            {
                return BadRequest(new Error(Messages.AUTHENTICATION_ERROR));
            }

            UserInfo userInfo = userReq.ConvertToUserInfo();

            UserInfo targetUserData = this._context.UserInfos
                .Where(ui => ui.MailAddress.Equals(userInfo.MailAddress)).FirstOrDefault();
            if (targetUserData == null)
            {
                return NotFound(new Error(Messages.EMAIL_CONFLICT));
            }

            this._context.UserInfos.Remove(targetUserData);
            await this._context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// ユーザーを作成する。
        /// POST: api/v1/user
        /// </summary>
        /// <param name="userReq"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostUser(UserReq userReq)
        {
            UserInfo userInfo = userReq.ConvertToUserInfo();

            if (this._context.UserInfoExists(userInfo.MailAddress))
            {
                return Conflict(new Error(Messages.EMAIL_CONFLICT));
            }

            this._context.UserInfos.Add(userInfo);
            await this._context.SaveChangesAsync();

            return Ok();
        }

        // For Debug
        // GET: api/user
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserInfo>>> GetUserInfos()
        {
            return await _context.UserInfos.ToListAsync();
        }
    }
}
