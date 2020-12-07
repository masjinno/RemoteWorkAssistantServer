using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RemoteWorkAssisntantServer.Dto
{
    public class PcIpAddressGetPutRes
    {
        [JsonPropertyName("email")]
        public string MailAddress { get; set; }

        [JsonPropertyName("pc")]
        public List<PcInfo> PcData { get; set; }
    }
}
