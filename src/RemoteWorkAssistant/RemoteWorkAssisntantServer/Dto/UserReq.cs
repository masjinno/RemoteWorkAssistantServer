using RemoteWorkAssisntantServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteWorkAssisntantServer.Dto
{
    public class UserReq : UserAuthorization
    {
        public UserInfo ConvertToUserInfo()
        {
            return new UserInfo
            {
                MailAddress = this.MailAddress,
                Password = this.Password
            };
        }
    }
}
