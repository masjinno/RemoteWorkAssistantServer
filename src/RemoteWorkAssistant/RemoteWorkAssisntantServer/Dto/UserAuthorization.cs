using Newtonsoft.Json;
using RemoteWorkAssisntantServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RemoteWorkAssisntantServer.Dto
{
    public abstract class UserAuthorization
    {
        [JsonProperty(PropertyName = "mailAddress", Required = Required.Always)]
        public string MailAddress { get; set; }

        [JsonProperty(PropertyName = "password", Required = Required.Always)]
        public string Password { get; set; }
    }
}
