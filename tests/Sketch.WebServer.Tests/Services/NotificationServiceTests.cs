using System;
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
        private readonly Mock<IHubClients> _clients;
        private readonly Mock<IClientProxy> _clientProxy;
        private readonly Mock<IGroupManager> _groups;

        private readonly Mock<IHubContext<SocialHub>> _context;
        private readonly Mock<IHubConnectionMapper<User>> _connections;
        private readonly Mock<IHubSubscriptionMapper<string>> _subscriptions;

        public NotificationServiceTests()
        {
            _clientProxy = new Mock<IClientProxy>();
            _clientProxy.Setup(mock => mock.SendCoreAsync(
                It.IsAny<string>(), It.IsAny<object[]>(), It.IsAny<CancellationToken>()));

            _groups = new Mock<IGroupManager>();
            _groups.Setup(mock => mock.AddToGroupAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));
            _groups.Setup(mock => mock.AddToGroupAsync(
                It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()));

            _clients = new Mock<IHubClients>();
            _clients.Setup(mock => mock.Group(It.IsAny<string>()))
                .Returns(_clientProxy.Object);
            _clients.Setup(mock => mock.Client(It.IsAny<string>()))
                .Returns(_clientProxy.Object);
            _clients.Setup(mock => mock.GroupExcept(It.IsAny<string>(), It.IsAny<IReadOnlyList<string>>()))
                .Returns(_clientProxy.Object);

            _context = new Mock<IHubContext<SocialHub>>();
            _context.SetupGet(mock => mock.Clients).Returns(_clients.Object);
            _context.SetupGet(mock => mock.Groups).Returns(_groups.Object);

            _connections = new Mock<IHubConnectionMapper<User>>();
            _connections.SetupAllProperties();

            _subscriptions = new Mock<IHubSubscriptionMapper<string>>();
            _subscriptions.SetupAllProperties();
        }

        [Fact]
        public async Task When_PublishAsync_Should_SendAsync()
        {
            // Arrange
            var groupName = "MyGroup";
            var groupContent = "MyContent";

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.PublishAsync(groupName, groupContent);

            // Assert
            _clientProxy.Verify(mock => mock.SendCoreAsync(
                It.Is<string>(value => value.Contains($"{typeof(string)}")),
                It.Is<object[]>(args => args.Contains(groupContent)),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task When_PublishAsync_Should_CaptureGroupChannel()
        {
            // Arrange
            var groupName = "MyGroup";
            var groupContent = "MyContent";

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.PublishAsync(groupName, groupContent);

            // Assert
            _clients.Verify(mock => mock.Group(groupName));
        }

        [Fact]
        public async Task When_WhisperAsync_Should_SendContent()
        {
            // Arrange
            var clientName = "MyClient";
            var clientConent = "MyContent";

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.WhisperAsync(clientName, clientConent);

            // Assert
            _clientProxy.Verify(mock => mock.SendCoreAsync(
                It.Is<string>(value => value.Contains($"{typeof(string)}")),
                It.Is<object[]>(args => args.Contains(clientConent)),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task When_WhisperAsync_Should_CaptureClientChannel()
        {
            // Arrange
            var clientName = "MyClient";
            var clientConent = "MyContent";

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.WhisperAsync(clientName, clientConent);

            // Assert
            _clients.Verify(mock => mock.Client(clientName));
        }

        [Fact]
        public async Task When_RegisterAsync_Should_StoreUserInfo()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.RegisterAsync(subscriberId, subscriber);

            // Assert
            _connections.Verify(mock => mock.AddAsync(subscriberId, subscriber));
        }

        [Fact]
        public async Task When_RegisterAsync_Should_StoreUserSubscriptions()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.RegisterAsync(subscriberId, subscriber);

            // Assert
            _subscriptions.Verify(mock => mock.AddSubscriberAsync(subscriberId));
        }

        [Fact]
        public async Task When_UnregisterAsync_Should_Publish()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            _subscriptions.Setup(mock => mock.GetSubscriptionsAsync(
                It.IsAny<string>())).Returns(Task.FromResult(
                new[] { "One", "Two", "Three" }.AsEnumerable()));

            var notifyService = new NotificationService(
               subscriptions: _subscriptions.Object,
               connections: _connections.Object,
               context: _context.Object);

            // Act
            await notifyService.UnregisterAsync(subscriberId);

            // Assert
            _clientProxy.Verify(mock => mock.SendCoreAsync(
                 It.Is<string>(value => value.Contains($"{typeof(UnsubscribeEvent)}")),
                 It.Is<object[]>(args => args.OfType<UnsubscribeEvent>().Any()),
                 It.IsAny<CancellationToken>()), Times.Exactly(3));
        }

        [Fact]
        public async Task When_UnregisterAsync_Should_RemoveFromGroups()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            _subscriptions.Setup(mock => mock.GetSubscriptionsAsync(
                It.IsAny<string>())).Returns(Task.FromResult(
                new[] { "One", "Two", "Three" }.AsEnumerable()));

            var notifyService = new NotificationService(
               subscriptions: _subscriptions.Object,
               connections: _connections.Object,
               context: _context.Object);

            // Act
            await notifyService.UnregisterAsync(subscriberId);

            // Assert
            _groups.Verify(mock => mock.RemoveFromGroupAsync(subscriberId,
                It.Is<string>(value => new[] { "One", "Two", "Three" }.Contains(value)),
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task When_UnregisterAsync_Should_DiscardUserInfo()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.RegisterAsync(subscriberId, subscriber);
            await notifyService.UnregisterAsync(subscriberId);

            // Assert
            _connections.Verify(mock => mock.RemoveAsync(subscriberId));
        }

        [Fact]
        public async Task When_UnregisterAsync_Should_DiscardUserSubscriptions()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.RegisterAsync(subscriberId, subscriber);
            await notifyService.UnregisterAsync(subscriberId);

            // Assert
            _subscriptions.Verify(mock => mock.RemoveSubscriberAsync(subscriberId));
        }

        [Fact]
        public async Task When_SubscribeAsync_Should_AddSubscription()
        {
            // Arrange
            var groupName = "MyChannel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.SubscribeAsync(subscriberId, groupName);

            // Assert
            _subscriptions.Verify(mock => mock.SubscribeAsync(subscriberId, groupName));
        }

        [Fact]
        public async Task When_SubscribeAsync_Should_AddToGroup()
        {
            // Arrange
            var groupName = "MyChannel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.SubscribeAsync(subscriberId, groupName);

            // Assert
            _groups.Verify(mock =>
                mock.AddToGroupAsync(subscriberId, groupName,
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task When_SubscribeAsync_Should_Publish()
        {
            // Arrange
            var groupName = "MyChannel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.SubscribeAsync(subscriberId, groupName);

            // Assert
            _clients.Verify(mock => mock.Group(groupName));
            _clientProxy.Verify(mock => mock.SendCoreAsync(
               It.Is<string>(value => value.Contains($"{typeof(SubscribeEvent)}")),
               It.Is<object[]>(args => args.OfType<SubscribeEvent>().Any()),
               It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task When_SubscribeAsync_Should_Whisper()
        {
            // Arrange
            var groupName = "MyChannel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            _subscriptions.Setup(mock => mock.GetSubscribersAsync(
              It.IsAny<string>())).Returns(Task.FromResult(
              new[] { "One", "Two", "Three" }.AsEnumerable()));

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.SubscribeAsync(subscriberId, groupName);

            // Assert
            _clients.Verify(mock => mock.Client(subscriberId), Times.Exactly(3));
            _clientProxy.Verify(mock => mock.SendCoreAsync(
               It.Is<string>(value => value.Contains($"{typeof(SubscribeEvent)}")),
               It.Is<object[]>(args => args.OfType<SubscribeEvent>().Any()),
               It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task When_UnsubscribeAsync_Should_RemoveSubscription()
        {
            // Arrange
            var groupName = "MyChannel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.UnsubscribeAsync(subscriberId, groupName);

            // Assert
            _subscriptions.Verify(mock => mock.UnsubscribeAsync(subscriberId, groupName));
        }

        [Fact]
        public async Task When_UnsubscribeAsync_Should_RemoveFromGroup()
        {
            // Arrange
            var groupName = "MyChannel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.UnsubscribeAsync(subscriberId, groupName);

            // Assert
            _groups.Verify(mock =>
                mock.RemoveFromGroupAsync(subscriberId, groupName,
                It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task When_UnsubscribeAsync_Should_Publish()
        {
            // Arrange
            var groupName = "MyChannel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.UnsubscribeAsync(subscriberId, groupName);

            // Assert
            _clients.Verify(mock => mock.Group(groupName));
            _clientProxy.Verify(mock => mock.SendCoreAsync(
               It.Is<string>(value => value.Contains($"{typeof(UnsubscribeEvent)}")),
               It.Is<object[]>(args => args.OfType<UnsubscribeEvent>().Any()),
               It.IsAny<CancellationToken>()));
        }

        [Fact]
        public async Task When_UnsubscribeAsync_Should_Whisper()
        {
            // Arrange
            var groupName = "MyChannel";
            var subscriberId = "abc-123-def-456";
            var subscriber = new User { Name = "MyName" };

            _subscriptions.Setup(mock => mock.GetSubscribersAsync(
              It.IsAny<string>())).Returns(Task.FromResult(
              new[] { "One", "Two", "Three" }.AsEnumerable()));

            var notifyService = new NotificationService(
                subscriptions: _subscriptions.Object,
                connections: _connections.Object,
                context: _context.Object);

            // Act
            await notifyService.UnsubscribeAsync(subscriberId, groupName);

            // Assert
            _clients.Verify(mock => mock.Client(subscriberId), Times.Exactly(3));
            _clientProxy.Verify(mock => mock.SendCoreAsync(
               It.Is<string>(value => value.Contains($"{typeof(UnsubscribeEvent)}")),
               It.Is<object[]>(args => args.OfType<UnsubscribeEvent>().Any()),
               It.IsAny<CancellationToken>()));
        }
    }
}
