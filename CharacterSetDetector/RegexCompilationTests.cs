using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace CharacterSetDetector
{
    [TestFixture]
    public class RegexCompilationTests
    {
        private TimeSpan _elapsedTime;

        [OneTimeSetUp]
        public void WhenCompilingTheRegex()
        {
            var stopwatch = Stopwatch.StartNew();
            var pattern = @"^[@£$¥èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞ\^{}\\[~\]|€ÆæßÉ!""#¤%&'()*+,\-.\/0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà\u000c\u0020\u000d\u000a]*$";
            new Regex(pattern, RegexOptions.Compiled);
            stopwatch.Stop();

            _elapsedTime = stopwatch.Elapsed;
        }

        [Test]
        public void ThenTheRegexIsCompiledInAnAcceptableLengthOfTime()
        {
            Assert.That(_elapsedTime, Is.LessThanOrEqualTo(TimeSpan.FromMilliseconds(100)));
        }
    }
}
