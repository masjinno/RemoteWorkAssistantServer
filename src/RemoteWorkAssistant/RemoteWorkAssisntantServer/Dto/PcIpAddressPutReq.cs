using RemoteWorkAssisntantServer.Models;
using System.Text.Json.Serialization;

namespace RemoteWorkAssisntantServer.Dto
{
    public class PcIpAddressPutReq : UserAuthorization
    {
        [JsonPropertyName("pcName")]
        public string PcName { get; set; }

        [JsonPropertyName("ipAddress")]
        public string IpAddress { get; set; }

        public PcRecord ConvertToPcRecord()
        {
            return new PcRecord
            {
                Id = RemoteWorkAssistantContext.GeneratePcInfoId(
                    this.MailAddress, this.PcName),
                MailAddress = this.MailAddress,
                PcName = this.PcName,
                IpAddress = this.IpAddress
            };
        }
    }
}
