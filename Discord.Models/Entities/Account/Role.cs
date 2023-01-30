using Newtonsoft.Json;

namespace Eynwa.Models.Entities.Account
{
    public class Role
    {
        [JsonProperty("id")]
        public uint Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
