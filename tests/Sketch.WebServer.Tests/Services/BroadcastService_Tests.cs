using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.SignalR;

using Moq;

using Sketch.Shared;
using Sketch.Shared.Data;
using Sketch.WebServer;
using Sketch.WebServer.Hubs;

using Xunit;

namespace Sketch.WebServer.Services.Tests
{
    public class BroadcastService_Tests
    {
        [Fact]
        public void When_BroadcastAsync_Should_SendContent()
        {
        }

        [Fact]
        public void When_BroadcastAsync_Should_SendToAllSubscriptions()
        {
        }

        [Fact]
        public void When_WhisperAsync_Should_SendContent()
        {
        }

        [Fact]
        public void When_WhisperAsync_Should_SendToSubscriber()
        {
        }

        [Fact]
        public void When_IdentifyAsync_Should_StoreUserInfo()
        {
        }

        [Fact]
        public void When_IdentifyAsync_Should_NotifyAllSubscriptions()
        {
        }

        [Fact]
        public void When_RegsisterAsync_Should_StoreUserSubscriptions()
        {
        }

        [Fact]
        public void When_UnregisterAsync_Should_DiscardUserInfo()
        {
        }

        [Fact]
        public void When_UnregisterAsync_Should_DiscardUserSubscriptions()
        {
        }

        [Fact]
        public void When_SubscribeAsync_Should_AddToGroup()
        {
        }

        [Fact]
        public void When_SubscribeAsync_Should_AddSubscription()
        {
        }

        [Fact]
        public void When_SubscribeAsync_Should_NotifyAllSubscribers()
        {
        }

        [Fact]
        public void When_UnsubscribeAsync_Should_RemoveFromGroup()
        {
        }

        [Fact]
        public void When_UnsubscribeAsync_Should_RemoveSubscription()
        {
        }

        [Fact]
        public void When_UnsubscribeAsync_Should_NotifyAllSubscribers()
        {
        }
    }
}
