using Newtonsoft.Json;
using RemoteWorkAssisntantServer.Models;

namespace RemoteWorkAssisntantServer.Dto
{
    public class PcPostReq : UserAuthorization
    {
        [JsonProperty(PropertyName = "pcName")]
        public string PcName { get; set; }

        public PcInfo ConvertToPcInfo()
        {
            return new PcInfo
            {
                Id = RemoteWorkAssistantContext.GeneratePcInfoId(
                    this.MailAddress, this.PcName),
                MailAddress = this.MailAddress,
                PcName = this.PcName,
                IpAddress = null
            };
        }
    }
}
