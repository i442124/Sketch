﻿using System;
using System.Threading;
using System.Threading.Tasks;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Hubs;

namespace Sketch.WebServer.Services
{
    public class WhiteboardService : IWhiteboardService
    {
        private readonly INotificationService _notifyService;

        public WhiteboardService(INotificationService notifyService)
        {
            _notifyService = notifyService;
        }

        public Task StrokeAsync(string channel, Stroke stroke)
        {
            return _notifyService.PublishAsync(channel, new StrokeEvent
            {
                Stroke = stroke, TimeStamp = DateTime.Now
            });
        }

        public Task WipeAsync(string channel, Wipe wipe)
        {
            return _notifyService.PublishAsync(channel, new WipeEvent
            {
                Wipe = wipe, Timestamp = DateTime.Now
            });
        }

        public Task FillAsync(string channel, Fill fill)
        {
            return _notifyService.PublishAsync(channel, new FillEvent
            {
                Fill = fill, TimeStamp = DateTime.Now
            });
        }

        public Task ClearAsync(string channel, Clear clear)
        {
            return _notifyService.PublishAsync(channel, new ClearEvent
            {
                Clear = clear, TimeStamp = DateTime.Now
            });
        }
    }
}
