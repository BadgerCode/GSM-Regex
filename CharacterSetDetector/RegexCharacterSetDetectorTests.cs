using System;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;

namespace CharacterSetDetector
{
    [TestFixture]
    public class RegexCharacterSetDetectorTests
    {
        [TestFixture]
        public class MessageBodyRegexCharacterSetDetectorOnlyGSMCharacterTests
        {
            [TestCase("abc")]
            [TestCase("ΔΔΔΔ")]
            [TestCase("\u0020")]
            [TestCase("\u000a")]
            [TestCase("\u000d")]
            [TestCase("ABCDEFGHIJK")]
            [TestCase("@£$¥èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞ^{}\\[~]|€ÆæßÉ!\"#¤%&'()*+,-./0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà")]
            [TestCase("\\\\\\\\\\")]
            [TestCase("\"\"\"\"")]
            [TestCase("\u0394")]
            [TestCase("\r\n")]
            public void ThenGSMIsReturned(string body)
            {
                Assert.That(new RegexCharacterSetDetector().Detect(body), Is.EqualTo(ResultCharacterSet.GSM));
            }
        }

        [TestFixture]
        public class MessageBodyRegexCharacterSetDetectorWithCharactersNotInTheGSMAlphabetTests
        {
            [TestCase("\u04ea")]
            [TestCase("\ud800\udc00")]
            [TestCase("\u0020\u04ea")]
            [TestCase("@£$¥èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞ^{}\\[~]|€ÆæßÉ!\"#¤%&'()*+,-./0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà\u0531")]
            [TestCase("Not unicode? Is únicode")]
            public void ThenUnicodeIsReturned(string body)
            {
                Assert.That(new RegexCharacterSetDetector().Detect(body), Is.EqualTo(ResultCharacterSet.Unicode));
            }
        }

        [TestFixture]
        public class GivenAGSMString
        {
            private ResultCharacterSet _result;

            [TestFixtureSetUp]
            public void WhenDetectingTheCharacterSet()
            {
                var detector = new RegexCharacterSetDetector();
                _result = detector.Detect("Hello world! Nice £");
            }

            [Test]
            public void ThenGSMIsReturned()
            {
                Assert.That(_result, Is.EqualTo(ResultCharacterSet.GSM));
            }
        }

        [TestFixture]
        public class GivenAGSMStringWithAnyGSMCharacter
        {
            private ResultCharacterSet _result;

            [TestFixtureSetUp]
            public void WhenDetectingTheCharacterSet()
            {
                var detector = new RegexCharacterSetDetector();
                _result = detector.Detect(@"@£$¥èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞ\^{}\\[~\]|€ÆæßÉ!""#¤%&'()*+,\-.\/0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà\u000c\u0020\u000d\u000a");
            }

            [Test]
            public void ThenUnicodeIsReturned()
            {
                Assert.That(_result, Is.EqualTo(ResultCharacterSet.GSM));
            }
        }

        [TestFixture]
        public class GivenAnEmptyString
        {
            private ResultCharacterSet _result;

            [TestFixtureSetUp]
            public void WhenDetectingTheCharacterSet()
            {
                var detector = new RegexCharacterSetDetector();
                _result = detector.Detect("");
            }

            [Test]
            public void ThenGSMIsReturned()
            {
                Assert.That(_result, Is.EqualTo(ResultCharacterSet.GSM));
            }
        }

        [TestFixture]
        public class Given50000GSMStringsOfLength918
        {
            private ResultCharacterSet[] _results;
            private TimeSpan _elapsedTime;

            [TestFixtureSetUp]
            public void WhenDetectingTheCharacterSet()
            {
                var testSize = 50000;
                var longString = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
                               + "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
                               + "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
                               + "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
                               + "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
                               + "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
                               + "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
                               + "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
                               + "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890"
                               + "123456789012345678";

                _results = new ResultCharacterSet[testSize];


                var detector = new RegexCharacterSetDetector();

                var stopWatch = Stopwatch.StartNew();
                for (var i = 0; i < testSize; i++)
                {
                    _results[i] = detector.Detect(longString);
                }
                stopWatch.Stop();

                _elapsedTime = stopWatch.Elapsed;
            }

            [Test]
            public void ThenGSMIsReturned()
            {
                var allResultsGSM = _results.All(result => result == ResultCharacterSet.GSM);
                Assert.That(allResultsGSM, Is.True);
            }

            [Test]
            public void ThenTheResultsAreReturnedInAnAcceptableAmountOfTime()
            {
                Assert.That(_elapsedTime, Is.LessThanOrEqualTo(TimeSpan.FromSeconds(2)));
            }
        }

        [TestFixture]
        public class GivenAUnicodeStringWithGSMLookalikeCharacters
        {
            [TestCase("\u2134")]// o
            [TestCase("\u014D")]// o
            [TestCase("\u017F")]// s
            [TestCase("\u0131")]// I
            [TestCase("\u0129")]// i
            [TestCase("\u212A")]// K
            [TestCase("\u0137")]// k
            [TestCase("\uFF21")]// A
            [TestCase("\u03B1")]// a
            [TestCase("\u02BA")]// "
            [TestCase("\u030E")]// "
            [TestCase("\uFF02")]// "
            [TestCase("\u02B9")]// '
            [TestCase("\u030D")]// '
            [TestCase("\uFF07")]// '
            [TestCase("\uFF1C")]// <
            [TestCase("\uFE64")]// <
            [TestCase("\u2329")]// <
            [TestCase("\u3008")]// <
            [TestCase("\u00AB")]// <
            [TestCase("\u00BB")]// >
            [TestCase("\u3009")]// >
            [TestCase("\u232A")]// >
            [TestCase("\uFE65")]// >
            [TestCase("\uFF1E")]// >
            [TestCase("\u2236")]// :
            [TestCase("\u0589")]// :
            [TestCase("\uFE13")]// :
            [TestCase("\uFE55")]// :
            [TestCase("\uFF1A")]// :
            public void ThenUnicodeIsReturned(string body)
            {
                Assert.That(new RegexCharacterSetDetector().Detect(body), Is.EqualTo(ResultCharacterSet.Unicode));
            }
        }
    }
}
