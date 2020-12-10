using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace Sketch.Shared.Services.Tests
{
    public class NotificationService_Tests
    {
        [Fact]
        public async Task When_InvokeAsync_Should_InvokeAll()
        {
            // Arrange
            var result_A = false;
            var result_B = false;

            var notificationService = new NotificationService();
            notificationService.Subscribe<bool>(value => Task.Run(() => result_A = value));
            notificationService.Subscribe<bool>(value => Task.Run(() => result_B = value));

            // Act
            await notificationService.InvokeAsync(true);

            // Assert
            Assert.True(result_A);
            Assert.True(result_B);
        }

        [Fact]
        public async Task When_InvokeAsync_Should_InvokeNone()
        {
            // Arrange
            var result_A = false;
            var result_B = false;

            var notificationService = new NotificationService();
            notificationService.Subscribe<bool>(value => Task.Run(() => result_A = value));
            notificationService.Subscribe<bool>(value => Task.Run(() => result_B = value));

            // Act
            await notificationService.InvokeAsync(1);
            await notificationService.InvokeAsync("MyValue");

            // Assert
            Assert.False(result_A);
            Assert.False(result_B);
        }

        [Fact]
        public async Task When_InvokeAsync_Should_InvokeOnlyOfType()
        {
            // Arrange
            var result_A = -1;
            var result_B = false;

            var notificationService = new NotificationService();
            notificationService.Subscribe<int>(value => Task.Run(() => result_A = value));
            notificationService.Subscribe<bool>(value => Task.Run(() => result_B = value));

            // Act
            await notificationService.InvokeAsync(2);

            // Assert
            Assert.Equal(2, result_A);
            Assert.False(result_B);
        }

        [Fact]
        public async Task When_DisposeSubscription_Should_InvokeNone()
        {
            // Arrange
            var notificationResult = 0;
            var notificationService = new NotificationService();
            var subscription = notificationService.Subscribe<int>(
                value => Task.Run(() => notificationResult += value));

            // Act
            await notificationService.InvokeAsync(2);
            await notificationService.InvokeAsync(2);

            subscription.Dispose();

            await notificationService.InvokeAsync(2);
            await notificationService.InvokeAsync(2);

            // Assert
            Assert.Equal(4, notificationResult);
        }
    }
}
