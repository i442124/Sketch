using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.SignalR;

using Moq;

using Sketch.Shared;
using Sketch.WebServer;
using Sketch.WebServer.Hubs;

using Xunit;


namespace Sketch.WebServer.Services
{
    public class NotificationServiceTests
    {
        private readonly NotificationService _service;

        private readonly Mock<IHubClients> _clients;
        private readonly Mock<IClientProxy> _clientProxy;
        private readonly Mock<IGroupManager> _groupManager;

        private readonly Mock<IHubContext<SocialHub>> _context;
        private readonly Mock<IHubConnectionMapper<User>> _connections;
        private readonly Mock<IHubSubscriptionMapper<string>> _subscriptions;

        public NotificationServiceTests()
        {
            _clientProxy = new Mock<IClientProxy>();
            _clientProxy.Setup(mock => mock.SendCoreAsync(
                It.IsAny<string>(), It.IsAny<object[]>(), It.IsAny<CancellationToken>()));

            _clients = new Mock<IHubClients>();
            _clients.Setup(mock => mock.Group(It.IsAny<string>()))
                .Returns(_clientProxy.Object);
            _clients.Setup(mock => mock.GroupExcept(It.IsAny<string>(), It.IsAny<IReadOnlyList<string>>()))
                .Returns(_clientProxy.Object);
            _clients.Setup(mock => mock.Client(It.IsAny<string>()))
                .Returns(_clientProxy.Object);

            _groupManager = new Mock<IGroupManager>();
            _groupManager.Setup(mock => mock.AddToGroupAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
            _groupManager.Setup(mock => mock.RemoveFromGroupAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

            _context = new Mock<IHubContext<SocialHub>>();
            _context.SetupGet(mock => mock.Clients).Returns(_clients.Object);
            _context.SetupGet(mock => mock.Groups).Returns(_groupManager.Object);

            _connections = new Mock<IHubConnectionMapper<User>>();
            _connections.SetupAllProperties();

            _subscriptions = new Mock<IHubSubscriptionMapper<string>>();
            _subscriptions.SetupAllProperties();

            _service = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);
        }

        [Fact]
        public async Task When_RegisterAsync_Should_StoreUserInfo()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            // Act
            await _service.RegisterAsync(subscriberId, subscriber);

            // Assert
            _connections.Verify(mock => mock.AddAsync(subscriberId, subscriber));
        }

        [Fact]
        public async Task When_RegisterAsync_Should_StoreSubscriptions()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            // Act
            await _service.RegisterAsync(subscriberId, subscriber);

            // Assert
            _subscriptions.Verify(mock => mock.AddSubscriberAsync(subscriberId));
        }

        [Fact]
        public async Task When_UnregisterAsync_Should_DiscardUserInfo()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            // Act
            await _service.RegisterAsync(subscriberId, subscriber);
            await _service.UnregisterAsync(subscriberId);

            // Assert
            _connections.Verify(mock => mock.RemoveAsync(subscriberId));
        }

        [Fact]
        public async Task When_UnregisterAsync_Should_DiscardUserSubscriptions()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            // Act
            await _service.RegisterAsync(subscriberId, subscriber);
            await _service.UnregisterAsync(subscriberId);

            // Assert
            _subscriptions.Verify(mock => mock.RemoveSubscriberAsync(subscriberId));
        }

        [Fact]
        public async Task When_SubscribeAsync_Should_AddSubscription()
        {
            // Arrange
            var groupName = "Channel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            // Act
            await _service.SubscribeAsync(subscriberId, groupName);

            // Assert
            _subscriptions.Verify(mock =>
                mock.SubscribeAsync(subscriberId, groupName));
        }

        [Fact]
        public async Task When_SubscribeAsync_Should_AddToGroup()
        {
            // Arrange
            var groupName = "Channel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            // Act
            await _service.SubscribeAsync(subscriberId, groupName);

            // Assert
            _groupManager.Verify(mock =>
                mock.AddToGroupAsync(subscriberId, groupName,
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task When_SubscribeAsync_Should_NotifyGroup()
        {
            // Arrange
            var groupName = "Channel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            // Act
            await _service.SubscribeAsync(subscriberId, groupName);

            // Assert
            _clientProxy.Verify(mock => mock.SendCoreAsync(

                It.Is<string>(value =>
                    value.Contains($"{typeof(UserEvent)}")),

                It.Is<object[]>(args =>
                    args.OfType<UserEvent>().Any(
                    x => x.Connected == true)),

                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task When_UnsubscribeAsync_Should_RemoveSubscription()
        {
            // Arrange
            var groupName = "Channel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            // Act
            await _service.SubscribeAsync(subscriberId, groupName);
            await _service.UnsubscribeAsync(subscriberId, groupName);

            // Assert
            _subscriptions.Verify(mock =>
                mock.UnsubscribeAsync(subscriberId, groupName));
        }

        [Fact]
        public async Task When_UnsubscribeAsync_Should_RemoveFromGroup()
        {
            // Arrange
            var groupName = "Channel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            // Act
            await _service.SubscribeAsync(subscriberId, groupName);
            await _service.UnsubscribeAsync(subscriberId, groupName);

            // Assert
            _groupManager.Verify(mock =>
                mock.RemoveFromGroupAsync(subscriberId, groupName,
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task When_UnsubscribeAsync_Should_NotifyGroup()
        {
            // Arrange
            var groupName = "Channel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            // Act
            await _service.SubscribeAsync(subscriberId, groupName);
            await _service.UnsubscribeAsync(subscriberId, groupName);

            // Assert
            _clientProxy.Verify(mock => mock.SendCoreAsync(

                It.Is<string>(value =>
                    value.Contains($"{typeof(UserEvent)}")),

                It.Is<object[]>(args =>
                    args.OfType<UserEvent>().Any(
                    x => x.Connected == false)),

                It.IsAny<CancellationToken>()));
        }
    }
}
