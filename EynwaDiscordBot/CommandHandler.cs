using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using System.Threading.Tasks;
using System;
using EynwaDiscordBot.Functions;
using System.Linq;
using Discord;
using System.Collections.Generic;
using EynwaDiscordBot.Models;
using Refit;
using Discord.Interop.Services;
using EynwaDiscordBot.Models.Constants;
using Eynwa.Interop.Services;
using System.Diagnostics;
using EynwaDiscordBot.Handlers;
using Eynwa.Models.Entities.Account;
using Eynwa.Models.Twitch;
using Eynwa.Models.Entities.Stats;

namespace EynwaDiscordBot
{
    class CommandHandler
    {
        private DiscordSocketClient _client;
        private CommandService _service;
        private List<GameUpdateDate> userInGameList = new List<GameUpdateDate>();
        IUserService userService;
        IStatsService statsService;
        ITwitchApiService twitchService;

        SocketGuild eynwaGuild;


        public CommandHandler(DiscordSocketClient client)
        {
            _client = client;
            _service = new CommandService();

            //_service.AddModulesAsync(Assembly.GetEntryAssembly());

            //TODO : UTIL Pour les stats a réactiver plus tard
            _client.MessageReceived += HandleCommandAsync;
            _client.UserJoined += _client_UserJoined;
            _client.ReactionAdded += _client_ReactionAdded;
            _client.ReactionRemoved += _client_ReactionRemoved;
            _client.CurrentUserUpdated += _client_CurrentUserUpdated;
            _client.GuildMemberUpdated += _client_GuildMemberUpdated;
            this.twitchService = RestService.For<ITwitchApiService>(SystemConstants.BaseTwitchApiUrl);


#if LOCAL //CONFIGURATION DEBUGLOCAL
            //this.userService = RestService.For<IUserService>("https://localhost:44398/api");
            //this.statsService = RestService.For<IStatsService>("https://localhost:44398/api");
#else
            //this.userService = RestService.For<IUserService>(SystemConstants.BaseUrl);
            //this.statsService = RestService.For<IStatsService>(SystemConstants.BaseUrl);
#endif
        }

        private Task _client_CurrentUserUpdated(SocketSelfUser arg1, SocketSelfUser arg2)
        {
            throw new NotImplementedException();
        }

        [Command(RunMode = RunMode.Async)]
        private async Task _client_GuildMemberUpdated(SocketGuildUser arg1, SocketGuildUser arg2)
        {
            if(arg1.Activity?.Type == ActivityType.Playing)
            {
                RichGame GameActivity = (RichGame)arg1.Activity;
                var gameSessions = GameActivity.GameSessions();

                if(arg1.Id == 1)
                {
                    GameInfo gameinfo = await this.twitchService.GetGameInfos("csgo");
                    SessionParameters parameters = new SessionParameters { BroadcasterLanguage = "fr", GameId = gameinfo.Id };
                    await this.twitchService.PatchSessions(parameters);

                }

                //TODO c'est quand je vais refaire les stats mdr
                //if(gameSessions?.TimePlayed > 0)
                //{
                //    gameSessions.UserDiscordId = arg1.Id;
                //    await statsService?.Create(gameSessions);
                //}
            }
        }

        private async Task HandleCommandAsync(SocketMessage s)
        {
            var msg = s as SocketUserMessage;
            if (msg == null) return;

            var context = new SocketCommandContext(_client, msg);

            int argPos = 0;
            if (msg.HasCharPrefix('!', ref argPos))
            {
                var result = await _service.ExecuteAsync(context, argPos, null);

                //await context.Channel.SendMessageAsync("Fanfreluche !!!! cette commande n'exite pas.");
                await context.Message.DeleteAsync();
            }
        }

        [Command(RunMode = RunMode.Async)]
        private async Task _client_UserJoined(SocketGuildUser arg)
        {
            if (arg == null) return;
            await Roles.GetInstance().AddRole(Roles.Joueur, arg);

            //Ajout du nouvel l'utilisateur dans la base (API)
            if (!arg.IsBot)
            {
                try
                {
                    var usersList = await this.userService?.GetAllUsers();
                    var results = usersList.Where(r => r.DiscordId == arg.Id);
                    if (results?.Count() != 0)
                    {
                        return;
                    }
                    else
                    {
                        await userService?.Create(new Models.Entities.Account.UserInfo
                        {
                            DiscordId = arg.Id,
                            Discriminator = arg.Discriminator,
                            Name = arg.Username
                        });

                        //TODO ADD ROLE
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        [Command(RunMode = RunMode.Async)]
        private async Task _client_ReactionAdded(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            this.eynwaGuild = _client.GetGuild(248520271357542410);
            var user = this.eynwaGuild.GetUser(arg3.UserId);
            //if (arg1.Id == 472563805306748938) //clique sur un icon de choix d'accès
            //{
            //    string emotName = arg3.Emote.Name;
            //    switch (emotName)
            //    {
            //        case "🎵":
            //            await Roles.GetInstance().AddRole(Roles.Dj, user);
            //            break;
            //        case "xam":
            //            var catTest = this.eynwaGuild.GetChannel(472563281802821643);
            //            OverwritePermissions testPermit = new OverwritePermissions(readMessageHistory: PermValue.Allow,
            //                                                                               readMessages: PermValue.Allow,
            //                                                                               sendMessages: PermValue.Allow,
            //                                                                               speak: PermValue.Allow,
            //                                                                               useVoiceActivation: PermValue.Allow,
            //                                                                               connect: PermValue.Allow);
            //            await catTest.AddPermissionOverwriteAsync(user, testPermit).ConfigureAwait(false);
            //            break;
            //        default:
            //            break;
            //    }
            //}
        }

        [Command(RunMode = RunMode.Async)]
        private async Task _client_ReactionRemoved(Cacheable<IUserMessage, ulong> arg1, ISocketMessageChannel arg2, SocketReaction arg3)
        {
            var eynwaGuild = this._client.GetGuild(248520271357542410);
            var user = eynwaGuild.GetUser(arg3.UserId);
            if (arg1.Id == 472563805306748938) //clique sur un icon de choix d'accès
            {
                string emotName = arg3.Emote.Name;
                switch (emotName)
                {
                    case "🎵":
                        await Roles.GetInstance().RemoveRole(Roles.Dj, user);
                        break;
                    case "xam":
                        var catTest = eynwaGuild.GetChannel(472563281802821643);
                        await catTest.RemovePermissionOverwriteAsync(user).ConfigureAwait(false);
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
