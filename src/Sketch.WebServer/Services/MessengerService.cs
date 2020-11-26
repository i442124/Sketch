﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Hubs;

namespace Sketch.WebServer.Services
{
    public class MessengerService : IMessengerService
    {
        private readonly INotificationService _notifyService;

        public MessengerService(INotificationService notifyService)
        {
            _notifyService = notifyService;
        }

        public Task SendAsync(string channel, string subscriberId, Message message)
        {
            return _notifyService.BroadcastAsync(channel, subscriberId, new MessageEvent
            {
                Message = message, TimeStamp = DateTime.Now
            });
        }
    }
}
