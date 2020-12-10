using System;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace Sketch.Shared.Data.Invocations.Tests
{
    public class Subscription_Tests
    {
        [Fact]
        public async Task When_Disposed_Should_RemoveHandlerFromList()
        {
            // Arrange
            var result = 0;
            var invocationHandler = new InvocationHandler(args => Task.Run(() => result++));
            var invocationHandlerList = new InvocationHandlerList(invocationHandler);
            var subscription = new Subscription(invocationHandler, invocationHandlerList);

            // Act
            await invocationHandlerList.InvokeAsync(null);
            await invocationHandlerList.InvokeAsync(null);
            await invocationHandlerList.InvokeAsync(null);

            subscription.Dispose();

            await invocationHandlerList.InvokeAsync(null);
            await invocationHandlerList.InvokeAsync(null);

            // Assert
            Assert.Equal(3, result);
        }

        [Fact]
        public async Task When_Disposed_Should_RemoveHandlerFromListTwice()
        {
            // Arrange
            var result = 0;
            var invocationHandler = new InvocationHandler(args => Task.Run(() => result++));
            var invocationHandlerList = new InvocationHandlerList(invocationHandler, invocationHandler);
            var subscription = new Subscription(invocationHandler, invocationHandlerList);

            // Act
            await invocationHandlerList.InvokeAsync(null);
            await invocationHandlerList.InvokeAsync(null);
            await invocationHandlerList.InvokeAsync(null);

            subscription.Dispose();

            await invocationHandlerList.InvokeAsync(null);
            await invocationHandlerList.InvokeAsync(null);

            subscription.Dispose();

            await invocationHandlerList.InvokeAsync(null);
            await invocationHandlerList.InvokeAsync(null);

            // Assert
            Assert.Equal(8, result);
        }
    }
}
