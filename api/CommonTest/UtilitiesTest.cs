using Common;

namespace CommonTest
{
    public class UtilitiesTest
    {
        [Fact]
        public void GetPattern_AllIncorrect()
        {
            // Arrange
            var guess = "ABACK";
            var answer = "HELLO";

            // Act
            var pattern = Utilities.GetPattern(guess, answer);

            // Assert
            Assert.Equal("XXXXX", pattern);
        }

        [Fact]
        public void GetPattern_AllCorrect()
        {
            // Arrange
            var guess = "HELLO";
            var answer = "HELLO";

            // Act
            var pattern = Utilities.GetPattern(guess, answer);

            // Assert
            Assert.Equal("GGGGG", pattern);
        }

        [Fact]
        public void GetPatter_FALSE_SULKY()
        {
            // Arrange
            var guess = "FALSE";
            var answer = "SULKY";

            // Act
            var pattern = Utilities.GetPattern(guess, answer);

            // Assert
            Assert.Equal("XXGYX", pattern);
        }

        [Fact]
        public void GetPattern_SPEED_ABIDE()
        {
            // Arrange
            var guess = "SPEED";
            var answer = "ABIDE";

            // Act
            var pattern = Utilities.GetPattern(guess, answer);

            // Assert
            Assert.Equal("XXYXY", pattern);
        }

        [Fact]
        public void GetPattern_SPEED_ERASE()
        {
            // Arrange
            var guess = "SPEED";
            var answer = "ERASE";

            // Act
            var pattern = Utilities.GetPattern(guess, answer);

            // Assert
            Assert.Equal("YXYYX", pattern);
        }

        [Fact]
        public void GetPattern_SPEED_STEAL()
        {
            // Arrange
            var guess = "SPEED";
            var answer = "STEAL";

            // Act
            var pattern = Utilities.GetPattern(guess, answer);

            // Assert
            Assert.Equal("GXGXX", pattern);
        }

        [Fact]
        public void GetPattern_SPEED_CREPE()
        {
            // Arrange
            var guess = "SPEED";
            var answer = "CREPE";

            // Act
            var pattern = Utilities.GetPattern(guess, answer);

            // Assert
            Assert.Equal("XYGYX", pattern);
        }
    }
}