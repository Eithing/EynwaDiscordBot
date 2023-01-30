using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;


namespace Eynwa.Models.Twitch
{
    public class GameInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("box_art_url")]
        public string BoxArtUrl { get; set; }
    }
}
