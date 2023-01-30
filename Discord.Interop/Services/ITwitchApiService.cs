using Eynwa.Models.Entities.Stats;
using Eynwa.Models.Twitch;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Eynwa.Interop.Services
{
    [Headers("Authorization:" + "Bearer bucs7svqf708gdmzaz0dvldsfkjaoo", "Client-Id:" + "l018hupxji48bv3mtxjkj38ibh468j")]
    public interface ITwitchApiService
    {
        [Patch("/channels?broadcaster_id=32743625")]
        Task PatchSessions([Body] SessionParameters param);

        [Get("/games?{name}")]
        Task<GameInfo> GetGameInfos(string name);
    }
}
