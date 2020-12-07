using Microsoft.EntityFrameworkCore;
using RemoteWorkAssisntantServer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWorkAssisntantServer.Models
{
    public class RemoteWorkAssistantContext : DbContext
    {
        public RemoteWorkAssistantContext(
            DbContextOptions<RemoteWorkAssistantContext> options) : base(options)
        {
        }

        public DbSet<UserRecord> UserTable { get; set; }
        public DbSet<PcRecord> PcTable { get; set; }

        public bool UserRecordExists(string mailAddress)
        {
            return this.UserTable.Any(e => e.MailAddress.Equals(mailAddress));
        }

        public bool PcRecordExists(string id)
        {
            return this.PcTable.Any(e => e.Id.Equals(id));
        }

        public static string GeneratePcInfoId(string mailAddress, string pcName)
        {
            string delimiter = "-";
            return new StringBuilder()
                .Append(mailAddress).Append(delimiter).Append(pcName).ToString();
        }

        public bool Authenticate(UserAuthorization userData)
        {
            return this.UserTable.Any(ui => ui.MailAddress.Equals(userData.MailAddress) && ui.Password.Equals(userData.Password));
        }
    }
}
