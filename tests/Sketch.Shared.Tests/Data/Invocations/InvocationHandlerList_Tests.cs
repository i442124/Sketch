using System;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace Sketch.Shared.Data.Invocations.Tests
{
    public class InvocationHandlerList_Tests
    {
        [Fact]
        public async Task When_InvokeAsync_InvokeAllCallback()
        {
            // Arrange
            var callbackResult_A = false;
            var callbackResult_B = false;
            var callbackResult_C = false;

            var invocationHandler_A = new InvocationHandler(args => Task.Run(() => callbackResult_A = true));
            var invocationHandler_B = new InvocationHandler(args => Task.Run(() => callbackResult_B = true));
            var invocationHandler_C = new InvocationHandler(args => Task.Run(() => callbackResult_C = true));
            var invocationHandlerList = new InvocationHandlerList(invocationHandler_A, invocationHandler_B, invocationHandler_C);

            // Act
            await invocationHandlerList.InvokeAsync(null);

            // Assert
            Assert.True(callbackResult_A);
            Assert.True(callbackResult_B);
            Assert.True(callbackResult_C);
        }

        [Fact]
        public async Task When_InvokeAsync_ShouldPassParametersToAll()
        {
            // Arrange
            var parameters = new object[] { 1, 2, 3 };
            var callbackResult_A = new object[] { -1 };
            var callbackResult_B = new object[] { -1 };
            var callbackResult_C = new object[] { -1 };

            var invocationHandler_A = new InvocationHandler(args => Task.Run(() => callbackResult_A = parameters));
            var invocationHandler_B = new InvocationHandler(args => Task.Run(() => callbackResult_B = parameters));
            var invocationHandler_C = new InvocationHandler(args => Task.Run(() => callbackResult_C = parameters));
            var invocationHandlerList = new InvocationHandlerList(invocationHandler_A, invocationHandler_B, invocationHandler_C);

            // Act
            await invocationHandlerList.InvokeAsync(null);

            // Assert
            Assert.Equal(callbackResult_A, parameters);
            Assert.Equal(callbackResult_B, parameters);
            Assert.Equal(callbackResult_C, parameters);
        }
    }
}
