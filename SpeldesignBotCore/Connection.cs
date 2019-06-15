﻿using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using SpeldesignBotCore.Entities;
using SpeldesignBotCore.Loggers;

namespace SpeldesignBotCore
{
    public class Connection
    {
        private readonly DiscordSocketClient _client;
        private readonly StatusLogger _statusLogger;
        private readonly DiscordMessageHandler _messageHandler;

        public Connection(DiscordSocketClient client, StatusLogger statusLogger, DiscordMessageHandler messageHandler)
        {
            _client = client;
            _statusLogger = statusLogger;
            _messageHandler = messageHandler;
        }

        internal async Task ConnectAsync(BotConfiguration config)
        {
            _client.Log += _statusLogger.LogToConsole;
            await _client.LoginAsync(TokenType.Bot, config.Token);
            await _client.StartAsync();

            _client.MessageReceived += _messageHandler.HandleMessageSentAsync;
            _client.MessageUpdated += _messageHandler.HandleMessageEditedAsync;

            await _client.SetGameAsync($"\"{config.Prefix}help\"", type: ActivityType.Listening);

            // Temporary 
            await _client.GetGuild(458191009084604417).GetTextChannel(526166967850106881).SendMessageAsync("Back online.");

            await Task.Delay(-1);
        }
    }
}
