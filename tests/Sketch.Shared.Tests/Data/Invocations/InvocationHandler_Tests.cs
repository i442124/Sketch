using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace Sketch.Shared.Data.Invocations.Tests
{
    public class InvocationHandler_Tests
    {
        [Fact]
        public async Task When_InvokeAsync_Should_InvokeCallback()
        {
            // Arrange
            var invocationCalled = false;
            var invocationHandler = new InvocationHandler(
                args => Task.Run(() => invocationCalled = true));

            // Act
            await invocationHandler.InvokeAsync(null);

            // Assert
            Assert.True(invocationCalled);
        }

        [Fact]
        public async Task When_InvokeAsync_Should_PassParameters()
        {
            // Arrange
            var invocationParametersResult = new object[] { 0 };
            var invocationParameters = new object[] { 1, 2, 3 };
            var invocationHandler = new InvocationHandler(
                args => Task.Run(() => invocationParametersResult = args));

            // Act
            await invocationHandler.InvokeAsync(invocationParameters);

            // Assert
            Assert.Equal(invocationParameters, invocationParametersResult);
        }
    }
}
