using System.Reflection;
using Newtonsoft.Json;

namespace CommonTest
{
    public abstract record TestDataObj(string Answer, string Guess, string Pattern);

    public static class TestingUtils
    {
        public static List<TestDataObj> GetTestData()
        {
            return JsonConvert.DeserializeObject<List<TestDataObj>>(
                File.ReadAllText(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty, @"Data/TestData.json")));
        }

        public static Dictionary<(string, string), string> BuildTestDataMap()
        {
            var map = new Dictionary<(string, string), string>();



            foreach (var test in GetTestData())
            {
                map[(test.Guess, test.Answer)] = test.Pattern;
            }

            return map;
        }
    }
}