using System;
using System.Threading;

using Xunit;

namespace Sketch.WebServer.Services
{
    public class HubSubscriptionMapperTests
    {
        [Fact]
        public void Should_InitializeSet_When_AddSubscriber()
        {
            // Arrange
            var subscriberId_A = "abc-123-def-456";
            var subscriberId_B = "def-456-ghi-789";
            var subscriptions = new HubSubscriptionMapper<string>();

            // Act
            subscriptions.AddSubscriber(subscriberId_A);
            subscriptions.AddSubscriber(subscriberId_B);

            // Assert
            Assert.Equal(2, subscriptions.SubscriberCount);
            Assert.Empty(subscriptions.GetSubscriptions(subscriberId_A));
            Assert.Empty(subscriptions.GetSubscriptions(subscriberId_B));
        }

        [Fact]
        public void Should_Deconstruct_When_RemoveSubscriber()
        {
            // Arrange
            var subscriberId_A = "abc-123-def-456";
            var subscriberId_B = "def-456-ghi-789";
            var subscriptions = new HubSubscriptionMapper<string>();

            // Act
            subscriptions.AddSubscriber(subscriberId_A);
            subscriptions.Subscribe(subscriberId_A, "SubscriptionA");
            subscriptions.Subscribe(subscriberId_A, "SubscriptionB");
            subscriptions.Subscribe(subscriberId_A, "SubscriptionC");

            subscriptions.AddSubscriber(subscriberId_B);
            subscriptions.Subscribe(subscriberId_B, "SubscriptionB");
            subscriptions.Subscribe(subscriberId_B, "SubscriptionC");

            subscriptions.RemoveSubscriber(subscriberId_A);

            // Assert
            Assert.Equal(1, subscriptions.SubscriberCount);
            Assert.Equal(2, subscriptions.SubscriptionCount);
            Assert.Contains("SubscriptionB", subscriptions);
            Assert.Contains("SubscriptionC", subscriptions);
        }

        [Fact]
        public void Should_AddSubscriber_When_Subscribe()
        {
            // Arrange
            var subscriberId_A = "abc-123-def-456";
            var subscriberId_B = "def-456-ghi-789";
            var subscriptions = new HubSubscriptionMapper<string>();

            // Act
            subscriptions.AddSubscriber(subscriberId_A);
            subscriptions.Subscribe(subscriberId_A, "SubscriptionA");
            subscriptions.Subscribe(subscriberId_A, "SubscriptionB");
            subscriptions.Subscribe(subscriberId_A, "SubscriptionC");

            subscriptions.AddSubscriber(subscriberId_B);
            subscriptions.Subscribe(subscriberId_B, "SubscriptionB");
            subscriptions.Subscribe(subscriberId_B, "SubscriptionC");

            // Assert
            Assert.Equal(2, subscriptions.SubscriberCount);
            Assert.Equal(3, subscriptions.SubscriptionCount);
            Assert.Contains(subscriberId_A, subscriptions.GetSubscribers("SubscriptionA"));
            Assert.Contains(subscriberId_A, subscriptions.GetSubscribers("SubscriptionB"));
            Assert.Contains(subscriberId_A, subscriptions.GetSubscribers("SubscriptionC"));
            Assert.Contains(subscriberId_B, subscriptions.GetSubscribers("SubscriptionB"));
            Assert.Contains(subscriberId_B, subscriptions.GetSubscribers("SubscriptionC"));
        }

        [Fact]
        public void Should_AddSubscription_When_Subscribe()
        {
            // Arrange
            var subscriberId_A = "abc-123-def-456";
            var subscriberId_B = "def-456-ghi-789";
            var subscriptions = new HubSubscriptionMapper<string>();

            // Act
            subscriptions.AddSubscriber(subscriberId_A);
            subscriptions.Subscribe(subscriberId_A, "SubscriptionA");
            subscriptions.Subscribe(subscriberId_A, "SubscriptionB");
            subscriptions.Subscribe(subscriberId_A, "SubscriptionC");

            subscriptions.AddSubscriber(subscriberId_B);
            subscriptions.Subscribe(subscriberId_B, "SubscriptionB");
            subscriptions.Subscribe(subscriberId_B, "SubscriptionC");

            // Assert
            Assert.Equal(2, subscriptions.SubscriberCount);
            Assert.Equal(3, subscriptions.SubscriptionCount);
            Assert.Contains("SubscriptionA", subscriptions.GetSubscriptions(subscriberId_A));
            Assert.Contains("SubscriptionB", subscriptions.GetSubscriptions(subscriberId_A));
            Assert.Contains("SubscriptionC", subscriptions.GetSubscriptions(subscriberId_A));
            Assert.Contains("SubscriptionB", subscriptions.GetSubscriptions(subscriberId_B));
            Assert.Contains("SubscriptionC", subscriptions.GetSubscriptions(subscriberId_B));
        }

        [Fact]
        public void Should_RemoveSubscriber_When_Unsubscribe()
        {
            // Arrange
            var subscriberId_A = "abc-123-def-456";
            var subscriberId_B = "def-456-ghi-789";
            var subscriptions = new HubSubscriptionMapper<string>();

            // Act
            subscriptions.AddSubscriber(subscriberId_A);
            subscriptions.Subscribe(subscriberId_A, "SubscriptionA");
            subscriptions.Subscribe(subscriberId_A, "SubscriptionB");

            subscriptions.AddSubscriber(subscriberId_B);
            subscriptions.Subscribe(subscriberId_B, "SubscriptionA");
            subscriptions.Subscribe(subscriberId_B, "SubscriptionB");

            subscriptions.Unsubscribe(subscriberId_A, "SubscriptionA");
            subscriptions.Unsubscribe(subscriberId_B, "SubscriptionB");

            // Assert
            Assert.Equal(2, subscriptions.SubscriberCount);
            Assert.Contains(subscriberId_A, subscriptions.GetSubscribers("SubscriptionB"));
            Assert.Contains(subscriberId_B, subscriptions.GetSubscribers("SubscriptionA"));
        }

        [Fact]
        public void Should_RemoveSubscription_When_Unsubscribe()
        {
            // Arrange
            var subscriberId_A = "abc-123-def-456";
            var subscriberId_B = "def-456-ghi-789";
            var subscriptions = new HubSubscriptionMapper<string>();

            // Act
            subscriptions.AddSubscriber(subscriberId_A);
            subscriptions.Subscribe(subscriberId_A, "SubscriptionA");
            subscriptions.Subscribe(subscriberId_A, "SubscriptionB");

            subscriptions.AddSubscriber(subscriberId_B);
            subscriptions.Subscribe(subscriberId_B, "SubscriptionA");
            subscriptions.Subscribe(subscriberId_B, "SubscriptionB");

            subscriptions.Unsubscribe(subscriberId_A, "SubscriptionA");
            subscriptions.Unsubscribe(subscriberId_B, "SubscriptionB");

            // Assert
            Assert.Equal(2, subscriptions.SubscriberCount);
            Assert.Contains("SubscriptionB", subscriptions.GetSubscriptions(subscriberId_A));
            Assert.Contains("SubscriptionA", subscriptions.GetSubscriptions(subscriberId_B));
        }

        [Fact]
        public void Should_ThrowException_When_AddSubscriber_On_DuplicateEntry()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriptions = new HubSubscriptionMapper<string>();

            // Act
            subscriptions.AddSubscriber(subscriberId);

            // Assert
            Assert.Equal(1, subscriptions.SubscriberCount);
            Assert.Throws<ArgumentException>(() => subscriptions.AddSubscriber(subscriberId));
        }

        [Fact]
        public void Should_ThrowException_When_Subscribe_On_DuplicateEntry()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriptions = new HubSubscriptionMapper<string>();

            // Act
            subscriptions.AddSubscriber(subscriberId);
            subscriptions.Subscribe(subscriberId, "Subscription");

            // Assert
            Assert.Equal(1, subscriptions.SubscriberCount);
            Assert.Equal(1, subscriptions.SubscriptionCount);
            Assert.Throws<ArgumentException>(() => subscriptions.Subscribe(subscriberId, "Subscription"));
        }

        [Fact]
        public void Should_ThrowException_When_Subscribe_On_InvalidSubscriber()
        {
            // Arrange
            var subscriberId_A = "abc-123-def-456";
            var subscriberId_B = "def-456-ghi-789";
            var subscriptions = new HubSubscriptionMapper<string>();

            // Act
            subscriptions.AddSubscriber(subscriberId_A);
            subscriptions.Subscribe(subscriberId_A, "Subscription");

            // Assert
            Assert.Equal(1, subscriptions.SubscriberCount);
            Assert.Throws<ArgumentException>(() => subscriptions.Subscribe(subscriberId_B, "Subscription"));
        }

        [Fact]
        public void Should_ThrowException_When_RemoveSubscriber_On_MissingEntry()
        {
            // Arrange
            var subscriberId_A = "abc-123-def-456";
            var subscriberId_B = "def-456-ghi-789";
            var subscriptions = new HubSubscriptionMapper<string>();

            // Act
            subscriptions.AddSubscriber(subscriberId_A);

            // Assert
            Assert.Equal(1, subscriptions.SubscriberCount);
            Assert.Throws<ArgumentException>(() => subscriptions.RemoveSubscriber(subscriberId_B));
        }

        [Fact]
        public void Should_ThrowException_When_Unsubscribe_On_MissingEntry()
        {
            // Arrange
            var subscriberId = "abc-123-def-456";
            var subscriptions = new HubSubscriptionMapper<string>();

            // Act
            subscriptions.AddSubscriber(subscriberId);
            subscriptions.Subscribe(subscriberId, "SubscriptionA");

            // Assert
            Assert.Equal(1, subscriptions.SubscriberCount);
            Assert.Equal(1, subscriptions.SubscriptionCount);
            Assert.Throws<ArgumentException>(() => subscriptions.Unsubscribe(subscriberId, "SubscriptionB"));
        }

        [Fact]
        public void Should_ThrowException_When_Unsubscribe_On_InvalidSubscriber()
        {
            // Arrange
            var subscriberId_A = "abc-123-def-456";
            var subscriberId_B = "def-456-ghi-789";
            var subscriptions = new HubSubscriptionMapper<string>();

            // Act
            subscriptions.AddSubscriber(subscriberId_A);
            subscriptions.Subscribe(subscriberId_A, "Subscription");

            // Assert
            Assert.Equal(1, subscriptions.SubscriberCount);
            Assert.Throws<ArgumentException>(() => subscriptions.Unsubscribe(subscriberId_B, "Subscription"));
        }
    }
}
