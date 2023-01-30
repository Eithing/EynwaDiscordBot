using Discord;
using Eynwa.Models.Entities.Stats;
using System;

namespace EynwaDiscordBot.Handlers
{
    public static class UserActivity
    {
        public static GameSessions GameSessions(this RichGame richGame)
        {
            if(richGame.ApplicationId != null)
            {
                int timePlayed = (DateTime.Now - richGame.Timestamps.Start.Value.DateTime).Minutes;
                if(timePlayed > 0)
                {
                    //TODO create game in base if not exist and get Game infos if exist
                    Eynwa.Models.Entities.Stats.Game newGame = new Eynwa.Models.Entities.Stats.Game { Name = richGame.Name, GameDiscordId = richGame.ApplicationId };
                    return new GameSessions
                    {
                        Game = newGame,
                        TimePlayed = timePlayed,
                        Date = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                    };
                }
            }
#if Local

#endif
            return null;
        }
    }
}
