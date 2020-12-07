using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RemoteWorkAssisntantServer.Models
{
    public class UserRecord
    {
        /// <summary>
        /// メールアドレス。主キー。
        /// ユーザー判別。
        /// </summary>
        [Key]
        public string MailAddress { get; set; }
        public string Password { get; set; }
    }
}
