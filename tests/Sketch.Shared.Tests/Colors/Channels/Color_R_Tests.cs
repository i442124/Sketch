using Xunit;

namespace Sketch.Shared
{
    public class Color_R_Tests
    {
        [Theory]
        [InlineData(000.0, 0xFF)]
        [InlineData(090.0, 0x80)]
        [InlineData(180.0, 0x00)]
        [InlineData(270.0, 0x7F)]
        [InlineData(360.0, 0xFF)]
        public void Should_Update_RedChannel_When_Adjusting_Hue(double hue, byte expected)
        {
            // Arrange
            var color = Color.FromRGB(255, 0, 0);

            //Act
            color.Hue = hue;

            // Assert
            Assert.Equal(expected, color.R);
        }

        [Theory]
        [InlineData(1.0, 0xFF)]
        [InlineData(0.8, 0xE6)]
        [InlineData(0.4, 0xB3)]
        [InlineData(0.0, 0x80)]
        public void Should_Update_RedChannel_When_Adjusting_Saturation(double saturation, byte expected)
        {
            // Arrange
            var color = Color.FromRGB(255, 128, 0);

            //Act
            color.Saturation = saturation;

            // Assert
            Assert.Equal(expected, color.R);
        }

        [Theory]
        [InlineData(1.0, 0xFF)]
        [InlineData(0.7, 0xFF)]
        [InlineData(0.4, 0xCC)]
        [InlineData(0.0, 0x00)]
        public void Should_Update_RedChannel_When_Adjusting_Luminosity(double luminosity, byte expected)
        {
            // Arrange
            var color = Color.FromRGB(255, 128, 0);

            //Act
            color.Luminosity = luminosity;

            // Assert
            Assert.Equal(expected, color.R);
        }
    }
}
