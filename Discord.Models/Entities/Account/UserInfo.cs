using Eynwa.Models.Entities.Account;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace EynwaDiscordBot.Models.Entities.Account
{
    public class UserInfo
    {
        [JsonProperty("id")]
        public uint Id { get; set; }

        [JsonProperty("discordId")]
        public ulong DiscordId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("discriminator")]
        public string Discriminator { get; set; }

        [JsonProperty("roles")]
        public List<Role> Roles { get; set; }
    }
}
