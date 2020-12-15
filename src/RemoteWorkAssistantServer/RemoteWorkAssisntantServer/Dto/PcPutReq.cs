using RemoteWorkAssisntantServer.Models;
using System.Text.Json.Serialization;

namespace RemoteWorkAssisntantServer.Dto
{
    public class PcPutReq : UserAuthorization
    {
        [JsonPropertyName("prePcName")]
        public string PrePcName { get; set; }

        [JsonPropertyName("newPcName")]
        public string NewPcName { get; set; }

        public PcRecord ConvertToPcRecord()
        {
            return new PcRecord
            {
                Id = RemoteWorkAssistantContext.GeneratePcInfoId(
                    this.MailAddress, this.PrePcName),
                MailAddress = this.MailAddress,
                PcName = this.NewPcName
            };
        }
    }
}
