using Newtonsoft.Json;
using System;

namespace Eynwa.Models.Entities.Stats
{
    public class GameSessions
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }

        [JsonProperty("game")]
        public Game Game { get; set; }

        [JsonProperty("timing")]
        public int TimePlayed { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("userId")]
        public ulong UserDiscordId { get; set; }
    }
}
