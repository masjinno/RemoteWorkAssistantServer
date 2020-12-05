using Newtonsoft.Json;
using RemoteWorkAssisntantServer.Models;

namespace RemoteWorkAssisntantServer.Dto
{
    public class PcPutReq : UserAuthorization
    {
        [JsonProperty(PropertyName = "pcName", Required = Required.Always)]
        public string PcName { get; set; }

        [JsonProperty(PropertyName = "ipAddress", Required = Required.Always)]
        public string IpAddress { get; set; }
        public PcInfo ConvertToPcInfo()
        {
            return new PcInfo
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
