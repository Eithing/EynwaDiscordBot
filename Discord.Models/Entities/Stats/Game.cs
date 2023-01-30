using Newtonsoft.Json;

namespace Eynwa.Models.Entities.Stats
{
    public class Game
    {
        [JsonProperty("id")]
        public ulong Id { get; set; }

        [JsonProperty("gameDiscordid")]
        public ulong GameDiscordId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
