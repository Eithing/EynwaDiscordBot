using Discord.Commands;
using Discord.WebSocket;
using Eynwa.Interop.Services;
using Eynwa.Models.Entities.Stats;
using EynwaDiscordBot.Models.Constants;
using Refit;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EynwaDiscordBot.Modules
{
    public class SessionsStats : ModuleBase<SocketCommandContext>
    {
        IStatsService statsService;

        public SessionsStats()
        {
            this.statsService = RestService.For<IStatsService>(SystemConstants.BaseUrl); // "http://91.121.178.28:5009/api");

        }

        [Command("Rank", RunMode = RunMode.Async)]
        public async Task rank()
        {
            var user = Context.Message.Author;
            string lastMonday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1).ToString("dd/MM/yyyy HH:mm:ss");
            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {
                lastMonday = DateTime.Today.ToString("dd/MM/yyyy HH:mm:ss");
            }

            var sessions = await this.statsService.GetAllSessions(dateStart : lastMonday, dateEnd : DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")); //get sessions from the last 7 days
            List <GameSessions> unifyUserList = new List<GameSessions>();
            var sessionsOfUser = sessions.Where(s => s.UserDiscordId == user.Id).ToList();
            var totalMinutesOfWeekForUser = sessionsOfUser.Sum(s => s.TimePlayed);

            foreach (var session in sessions)
            {
                if (unifyUserList.Any(i => i.UserDiscordId == session.UserDiscordId))
                {
                    //SI unifyUserList contient déjà l'utilisateur
                    foreach (var sessionUnify in unifyUserList)
                    {
                        if (sessionUnify.UserDiscordId == session.UserDiscordId)
                        {
                            sessionUnify.TimePlayed = sessionUnify.TimePlayed + session.TimePlayed;
                        }
                    }
                }
                else
                {
                    unifyUserList.Add(session);
                }
            }

            var rankingList = unifyUserList.OrderByDescending(t => t.TimePlayed).ToList();
            int position = rankingList.FindIndex(a => a.UserDiscordId == user.Id) + 1;

            if (position < 1)
            {
                await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} Tu n'es pas classé(e), laisse ta vie de côté pour ça !");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} Tu es classé(e) {position} sur {rankingList.Count} avec un total de {MinutesToHoursConverter(totalMinutesOfWeekForUser)}.");
            }
        }

        [Command("TopGame", RunMode = RunMode.Async)]
        public async Task topGame()
        {
            string lastMonday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1).ToString("dd/MM/yyyy HH:mm:ss");
            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {
                lastMonday = DateTime.Today.ToString("dd/MM/yyyy HH:mm:ss");
            }
            var sessions = await this.statsService.GetAllSessions(dateStart: lastMonday, dateEnd: DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")); //get sessions from the last 7 days

            List<GameSessions> unifyGameList = new List<GameSessions>();
            foreach (var session in sessions)
            {
                if (unifyGameList.Any(i => i.Game == session.Game))
                {
                    //SI unifyUserList contient déjà l'utilisateur
                    foreach (var sessionUnify in unifyGameList)
                    {
                        if (sessionUnify.Game == session.Game)
                        {
                            sessionUnify.TimePlayed = sessionUnify.TimePlayed + session.TimePlayed;
                        }
                    }
                }
                else
                {
                    unifyGameList.Add(session);
                }
            }

            var rankingList = unifyGameList.OrderByDescending(t => t.TimePlayed).ToList();
            if(rankingList.Count < 5)
            {
                await Context.Channel.SendMessageAsync("Il faut un minimum de 5 jeux jouer pour avoir un classement !");
            }
            
            else
            {
                await Context.Channel.SendMessageAsync($"Top Game de la semaine (début lundi minuit) : \n\n" +
                $":first_place: {rankingList[0]?.Game.Name} avec {MinutesToHoursConverter(rankingList[0]?.TimePlayed)}. \n" +
                $":second_place: {rankingList[1]?.Game.Name} avec {MinutesToHoursConverter(rankingList[1]?.TimePlayed)}. \n" +
                $":third_place: {rankingList[2]?.Game.Name} avec {MinutesToHoursConverter(rankingList[2]?.TimePlayed)}. \n" +
                $":four: {rankingList[3]?.Game.Name} avec {MinutesToHoursConverter(rankingList[3]?.TimePlayed)}.\n" +
                $":five: {rankingList[4]?.Game.Name} avec {MinutesToHoursConverter(rankingList[4]?.TimePlayed)}.\n\n" +
                $"Un total de {rankingList.Count} jeux ont été lancés.");
            }
        }

        [Command("TopPlayer", RunMode = RunMode.Async)]
        public async Task topPlayer()
        {
            string lastMonday = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1).ToString("dd/MM/yyyy HH:mm:ss");
            if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            {
                lastMonday = DateTime.Today.ToString("dd/MM/yyyy HH:mm:ss");
            }
            var sessions = await this.statsService.GetAllSessions(dateStart: lastMonday, dateEnd: DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")); //get sessions from the last 7 days

            List<GameSessions> unifyUserList = new List<GameSessions>();
            foreach (var session in sessions)
            {
                if (unifyUserList.Any(i => i.Id == session.Id))
                {
                    //SI unifyUserList contient déjà l'utilisateur
                    foreach (var sessionUnify in unifyUserList)
                    {
                        if (sessionUnify.Id == session.Id)
                        {
                            sessionUnify.TimePlayed = sessionUnify.TimePlayed + session.TimePlayed;
                        }
                    }
                }
                else
                {
                    unifyUserList.Add(session);
                }
            }

            var rankingList = unifyUserList.OrderByDescending(t => t.TimePlayed).ToList();
            if (rankingList.Count < 5)
            {
                await Context.Channel.SendMessageAsync("Il faut un minimum de 5 joueurs classer pour avoir un classement !");
            }
            else
            {
                await Context.Channel.SendMessageAsync($"Top joueurs de la semaine (début lundi minuit) : \n\n" +
                $":first_place: {this.Context.Guild.GetUser((ulong)rankingList[0]?.UserDiscordId).Mention} avec {MinutesToHoursConverter(rankingList[0]?.TimePlayed)}. \n" +
                $":second_place: {this.Context.Guild.GetUser((ulong)rankingList[1]?.UserDiscordId).Mention} avec {MinutesToHoursConverter(rankingList[1]?.TimePlayed)}. \n" +
                $":third_place: {this.Context.Guild.GetUser((ulong)rankingList[2]?.UserDiscordId).Mention} avec {MinutesToHoursConverter(rankingList[2]?.TimePlayed)}. \n" +
                $":four: {this.Context.Guild.GetUser((ulong)rankingList[3]?.UserDiscordId).Mention} avec {MinutesToHoursConverter(rankingList[3]?.TimePlayed)}.\n" +
                $":five: {this.Context.Guild.GetUser((ulong)rankingList[4]?.UserDiscordId).Mention} avec {MinutesToHoursConverter(rankingList[4]?.TimePlayed)}.\n\n" +
                $"Sur un total de {rankingList.Count} joueurs.");
            }
        }

        private string MinutesToHoursConverter(int? minutes)
        {
            if(minutes > 59)
            {

                int hour = int.Parse(minutes.ToString(), NumberStyles.Number) / 60;
                int minutesRecalculated = int.Parse(minutes.ToString(), NumberStyles.Number) % 60;
                if (hour > 1 && minutesRecalculated == 0)
                {
                    return $"{hour} heures";
                }
                if (hour < 2 && minutesRecalculated == 0)
                {
                    return $"{hour} heure";
                }
                if (hour > 1)
                {
                    return $"{hour} heures et {minutesRecalculated} minutes";
                }
                else
                {
                    return $"{hour} heure et {minutesRecalculated} minutes";
                }
            }
            else
            {
                return $"{minutes} minutes";
            }
        }
    }
}
