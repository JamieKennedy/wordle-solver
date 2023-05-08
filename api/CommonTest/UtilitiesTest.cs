using Common;

namespace CommonTest
{
    public class UtilitiesTest
    {
        [Fact]
        public void GetPattern()
        {
            // Assert
            var testData = TestingUtils.GetTestData();
            var actualMap = TestingUtils.BuildTestDataMap();

            // Act
            var resultsMap = new Dictionary<(string, string), string>();
            foreach (var test in testData)
            {
                resultsMap[(test.Guess, test.Answer)] = Utilities.GetPattern(test.Guess, test.Answer);
            }

            // Assert
            foreach (var entry in resultsMap)
            {
                Assert.Equal(entry.Value, actualMap[entry.Key]);
            }
        }
    }
}