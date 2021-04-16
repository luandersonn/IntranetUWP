﻿using IntranetUWP.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntranetUWP.Helpers
{
    public class IntranetSignalRHelper
    {
        private readonly HubConnection _hubConnection;
        public event Action<ChatMessageDTO> GeneralChatMessageReceived;
        public IntranetSignalRHelper(HubConnection hubConnection)
        {
            _hubConnection = hubConnection;
            _hubConnection.On<string, string>("ReceiveMessage", async (message, user) =>
            {
                ChatMessageDTO chatmessage = new ChatMessageDTO() { UserName = user, MessageContent = message };
                GeneralChatMessageReceived?.Invoke(chatmessage);
            });
        }

        public async Task ConnectAsync() => await _hubConnection.StartAsync();
        public async Task SendMessageAsync(string mess, int userId) => await _hubConnection.InvokeAsync("SendMessage", mess, userId);
    }
}
