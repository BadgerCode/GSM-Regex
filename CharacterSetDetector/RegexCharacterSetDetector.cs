using System.Text.RegularExpressions;

namespace CharacterSetDetector
{
    public class RegexCharacterSetDetector : ICharacterSetDetector
    {
        private readonly Regex _regex;

        public RegexCharacterSetDetector()
        {
            var pattern =
                @"^[@£$¥èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞ\^{}\\[~\]|€ÆæßÉ!""#¤%&'()*+,\-.\/0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà\u000c\u0020\u000d\u000a]*$";
            _regex = new Regex(pattern);
        }

        public ResultCharacterSet Detect(string text)
        {
            var isGSMOnly = _regex.IsMatch(text);
            return isGSMOnly ? ResultCharacterSet.GSM : ResultCharacterSet.Unicode;
        }
    }

    public class RegexCharacterSetDetectorUsingCompiledOption : ICharacterSetDetector
    {
        private readonly Regex _regex;

        public RegexCharacterSetDetectorUsingCompiledOption()
        {
            var pattern =
                @"^[@£$¥èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞ\^{}\\[~\]|€ÆæßÉ!""#¤%&'()*+,\-.\/0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà\u000c\u0020\u000d\u000a]*$";
            _regex = new Regex(pattern, RegexOptions.Compiled);
        }

        public ResultCharacterSet Detect(string text)
        {
            var isGSMOnly = _regex.IsMatch(text);
            return isGSMOnly ? ResultCharacterSet.GSM : ResultCharacterSet.Unicode;
        }
    }
}