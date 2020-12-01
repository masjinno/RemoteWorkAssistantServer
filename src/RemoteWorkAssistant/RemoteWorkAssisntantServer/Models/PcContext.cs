using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteWorkAssisntantServer.Models
{
    public class PcContext : DbContext
    {
        public PcContext(DbContextOptions<PcContext> options) : base(options)
        {
        }

        public DbSet<PcInfo> PcInfos { get; set; }
    }
}
