using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace RemoteWorkAssisntantServer.Dto
{
    public class PcInfo
    {
        [JsonPropertyName("pcName")]
        public string PcName { get; set; }

        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; }
    }
}
