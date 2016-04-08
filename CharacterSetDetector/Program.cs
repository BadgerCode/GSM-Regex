using System;
using System.Diagnostics;

namespace CharacterSetDetector
{
    /*
    | Number of strings | Normal   | Regex  | Compiled Regex |
    |===================|==========|========|================|
    | 1                 | 1ms      | 0ms    | 1ms            |
    | 5                 | 1ms      | 0ms    | 0ms            |
    | 10                | 3ms      | 0ms    | 0ms            |
    | 50                | 16ms     | 0ms    | 0ms            |
    | 100               | 33ms     | 1ms    | 0ms            |
    | 500               | 138ms    | 3ms    | 4ms            |
    | 1000              | 321ms    | 11ms   | 8ms            |
    | 10000             | 2436ms   | 118ms  | 68ms           |
    | 30000             | 6571ms   | 184ms  | 182ms          |
    | 50000             | 10057ms  | 329ms  | 257ms          |
    | 500000            | 103369ms | 2925ms | 2604ms         |
    */
    class Program
    {
        public static string OpeningMessage = "Opening message 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 charact";
        public static string Question1 = "Question 1 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 cha";
        public static string Question2 = "Question 2 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 cha";
        public static string Question3 = "Question 3 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 cha";
        public static string Question4 = "Question 4 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 cha";
        public static string Question5 = "Question 5 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 cha";
        public static string ClosingMessage = "Closing message 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 characters 918 charact";

        static void Main(string[] args)
        {
            SurveysTest();

            var testSizes = new[] {1, 5, 10, 50, 100, 500, 1000, 10000, 30000, 50000, 500000};
            foreach (var testSize in testSizes)
            {
                var characterSetDetector = new CharacterSetDetector();
                var regexCharacterSetDetector = new RegexCharacterSetDetector();
                var compiledRegexCharacterSetDetector = new RegexCharacterSetDetectorUsingCompiledOption();

                var testData = new string[testSize];
                for (var i = 0; i < testSize; i++)
                {
                    testData[i] =
                        "1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part 1 part";
                }

                TestSetDetector(characterSetDetector, "Normal", testData);
                TestSetDetector(regexCharacterSetDetector, "Regex", testData);
                TestSetDetector(compiledRegexCharacterSetDetector, "Compiled Regex", testData);
            }
            Console.Read();
        }

        public static void SurveysTest()
        {
            var characterSetDetector = new CharacterSetDetector();
            var compiledRegexCharacterSetDetector = new RegexCharacterSetDetectorUsingCompiledOption();

            TestCharacterSetOnSurveyTest(characterSetDetector, "Normal");
            TestCharacterSetOnSurveyTest(compiledRegexCharacterSetDetector, "Regex Compiled");
        }

        public static void TestCharacterSetOnSurveyTest(ICharacterSetDetector setDetector, string name)
        {
            var testSize = 50000;
            var characterSetDetectorStopWatch = Stopwatch.StartNew();
            for (var i = 0; i < testSize; i++)
            {
                setDetector.Detect(OpeningMessage);
                setDetector.Detect(Question1);
                setDetector.Detect(Question2);
                setDetector.Detect(Question3);
                setDetector.Detect(Question4);
                setDetector.Detect(Question5);
                setDetector.Detect(ClosingMessage);
            }
            characterSetDetectorStopWatch.Stop();

            Console.WriteLine($"Survey test. {name} took: {characterSetDetectorStopWatch.ElapsedMilliseconds}ms");
        }

        public static void TestSetDetector(ICharacterSetDetector setDetector, string name, string[] testData)
        {
            var testSize = testData.Length;
            var characterSetDetectorStopWatch = Stopwatch.StartNew();
            for (var i = 0; i < testSize; i++)
            {
                setDetector.Detect(testData[i]);
            }

            characterSetDetectorStopWatch.Stop();

            Console.WriteLine($"[{testSize}] {name} took: {characterSetDetectorStopWatch.ElapsedMilliseconds}ms");
        }
    }
}
