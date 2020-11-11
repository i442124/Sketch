using System;
using System.Threading;

using Xunit;

namespace Sketch.WebServer.Services
{
    public class HubConnectionMapperTests
    {
        [Fact]
        public void Should_StoreUserInfo_When_Add()
        {
            // Arrange
            var connectionId_A = "abc-123-def-456";
            var connectionId_B = "def-456-ghi-789";
            var connections = new HubConnectionMapper<string>();

            // Act
            connections.Add(connectionId_A, "UserA");
            connections.Add(connectionId_B, "UserB");

            // Assert
            Assert.Equal(2, connections.Count);
            Assert.Equal("UserA", connections.GetUserInfo(connectionId_A));
            Assert.Equal("UserB", connections.GetUserInfo(connectionId_B));
        }

        [Fact]
        public void Should_StoreUserInfo_When_Add_On_DuplicateEntry()
        {
            // Arrange
            var connectionId = "abc-123-def-456";
            var connections = new HubConnectionMapper<string>();

            // Act
            connections.Add(connectionId, "1st");
            connections.Add(connectionId, "2nd");
            connections.Add(connectionId, "3rd");
            connections.Add(connectionId, "4th");

            // Assert
            Assert.Equal(1, connections.Count);
            Assert.Equal("4th", connections.GetUserInfo(connectionId));
        }

        [Fact]
        public void Should_DiscardUserInfo_When_Remove()
        {
            // Arrange
            var connectionId_A = "abc-123-def-456";
            var connectionId_B = "def-456-ghi-789";
            var connections = new HubConnectionMapper<string>();

            // Act
            connections.Add(connectionId_A, "UserA");
            connections.Add(connectionId_B, "UserB");
            connections.Remove(connectionId_A);

            // Assert
            Assert.Equal(1, connections.Count);
            Assert.Equal("UserB", connections.GetUserInfo(connectionId_B));
        }

        [Fact]
        public void Should_ThrowException_When_Remove_On_MissingEntry()
        {
            // Arrange
            var connectionId_A = "abc-123-def-456";
            var connectionId_B = "def-456-ghi-789";
            var connections = new HubConnectionMapper<string>();

            // Act
            connections.Add(connectionId_A, "UserA");
            connections.Add(connectionId_B, "UserB");
            connections.Remove(connectionId_A);

            // Assert
            Assert.Equal(1, connections.Count);
            Assert.Throws<ArgumentException>(() => connections.Remove(connectionId_A));
        }

        [Fact]
        public void Should_ThrowException_When_GetUserInfo_On_MissingEntry()
        {
            // Arrange
            var connectionId_A = "abc-123-def-456";
            var connectionId_B = "def-456-ghi-789";
            var connections = new HubConnectionMapper<string>();

            // Act
            connections.Add(connectionId_A, "UserA");
            connections.Add(connectionId_B, "UserB");
            connections.Remove(connectionId_A);

            // Assert
            Assert.Equal(1, connections.Count);
            Assert.Throws<ArgumentException>(() => connections.GetUserInfo(connectionId_A));
        }
    }
}
