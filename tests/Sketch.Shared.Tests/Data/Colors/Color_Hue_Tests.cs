using Xunit;

namespace Sketch.Shared.Data.Ink.Colors.Tests
{
    public class Color_Hue_Tests
    {
        [Theory]
        [InlineData(0xFF, 060.0)]
        [InlineData(0x7F, 090.0)]
        [InlineData(0x00, 120.0)]
        public void Should_Update_Hue_When_Adjusting_RedChannel(byte red, double expected)
        {
            // Arrange
            var color = Color.FromHSL(60.0, 1.0, 0.5);

            //Act
            color.R = red;

            // Assert
            Assert.Equal(expected, color.Hue, 0);
        }

        [Theory]
        [InlineData(0xFF, 180.0)]
        [InlineData(0x7F, 210.0)]
        [InlineData(0x00, 240.0)]
        public void Should_Update_Hue_When_Adjusting_GreenChannel(byte green, double expected)
        {
            // Arrange
            var color = Color.FromHSL(180.0, 1.0, 0.5);

            //Act
            color.G = green;

            // Assert
            Assert.Equal(expected, color.Hue, 0);
        }

        [Theory]
        [InlineData(0xFF, 300.0)]
        [InlineData(0x7F, 330.0)]
        [InlineData(0x00, 000.0)]
        public void Should_Update_Hue_When_Adjusting_BlueChannel(byte blue, double expected)
        {
            // Arrange
            var color = Color.FromHSL(300.0, 1.0, 0.5);

            //Act
            color.B = blue;

            // Assert
            Assert.Equal(expected, color.Hue, 0);
        }
    }
}
