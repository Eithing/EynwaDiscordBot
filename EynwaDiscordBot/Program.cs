using System;
using Discord;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord.Audio;
using Discord.Interop.Services;
using Refit;

namespace EynwaDiscordBot
{
    class Program
    {
        static void Main(string[] args)
        => new Program().StartAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandHandler _handler;
        public async Task StartAsync()
        {
            _client = new DiscordSocketClient();
            await _client.SetGameAsync("Marabouter Freyja");
#if LOCAL //CONFIGURATION DEBUGLOCAL
            await _client.LoginAsync(TokenType.Bot, "*token*");
#else
            await _client.LoginAsync(TokenType.Bot, "*token*");
#endif
            await _client.StartAsync();
            _handler = new CommandHandler(_client);
            await Task.Delay(-1);
        }
    }
}
