using RemoteWorkAssisntantServer.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteWorkAssisntantServer.Models
{
    public class Error
    {
        public Error(Messages message)
        {
            this.Message = message.GetStringValue();
        }

        public string Message { get; set; }
    }
}
