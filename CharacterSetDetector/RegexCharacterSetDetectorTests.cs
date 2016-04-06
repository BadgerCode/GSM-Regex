using NUnit.Framework;

namespace CharacterSetDetector
{
    [TestFixture]
    public class RegexCharacterSetDetectorTests
    {
        [TestFixture]
        public class GivenAGSMString
        {
            private ResultCharacterSet _result;

            [OneTimeSetUp]
            public void WhenDetectingTheCharacterSet()
            {
                var detector = new RegexCharacterSetDetector();
                _result = detector.Detect("Hello world! Nice £");
            }

            [Test]
            public void ThenGSMIsReturnedAsTheType()
            {
                Assert.That(_result, Is.EqualTo(ResultCharacterSet.GSM));
            }
        }

        [TestFixture]
        public class GivenAStringWithUnicodeCharacters
        {
            [TestCase("\u04ea")]
            [TestCase("\ud800\udc00")]
            [TestCase("\u0020\u04ea")]
            [TestCase("Not unicode? Is únicode")]
            public void ThenTheGSMCharacterSetIsReturned(string body)
            {
                Assert.That(new RegexCharacterSetDetector().Detect(body), Is.EqualTo(ResultCharacterSet.Unicode));
            }
        }

        [TestFixture]
        public class GivenAGSMStringWithAnyGSMCharacter
        {
            private ResultCharacterSet _result;

            [OneTimeSetUp]
            public void WhenDetectingTheCharacterSet()
            {
                var detector = new RegexCharacterSetDetector();
                _result = detector.Detect(@"@£$¥èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞ\^{}\\[~\]|€ÆæßÉ!""#¤%&'()*+,\-.\/0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà\u000c\u0020\u000d\u000a");
            }

            [Test]
            public void ThenUnicodeIsReturnedAsTheType()
            {
                Assert.That(_result, Is.EqualTo(ResultCharacterSet.GSM));
            }
        }

        [TestFixture]
        public class GivenAnEmptyString
        {
            private ResultCharacterSet _result;

            [OneTimeSetUp]
            public void WhenDetectingTheCharacterSet()
            {
                var detector = new RegexCharacterSetDetector();
                _result = detector.Detect("");
            }

            [Test]
            public void ThenGSMIsReturnedAsTheType()
            {
                Assert.That(_result, Is.EqualTo(ResultCharacterSet.GSM));
            }
        }
    }
}
