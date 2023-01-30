using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eynwa.Models.Twitch
{
    public class SessionParameters
    {
        [JsonProperty("game_id")]
        public string GameId { get; set; }

        [JsonProperty("broadcaster_language")]
        public string BroadcasterLanguage { get; set; }
    }
}
