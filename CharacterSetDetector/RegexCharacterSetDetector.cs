using System.Text.RegularExpressions;

namespace CharacterSetDetector
{
    public static class CharacterSetRegex
    {
        public static Regex Expression;

        static CharacterSetRegex()
        {
            var pattern = @"^[@£$¥èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞ\^{}\\[~\]|€ÆæßÉ!""#¤%&'()*+,\-.\/0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà\u000c\u0020\u000d\u000a]*$";
            Expression = new Regex(pattern, RegexOptions.Compiled);
        }
    }

    public static class CharacterSetRegexCompiled
    {
        public static Regex Expression;

        static CharacterSetRegexCompiled()
        {
            var pattern = @"^[@£$¥èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞ\^{}\\[~\]|€ÆæßÉ!""#¤%&'()*+,\-.\/0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà\u000c\u0020\u000d\u000a]*$";
            Expression = new Regex(pattern, RegexOptions.Compiled);
        }
    }

    public class RegexCharacterSetDetector : ICharacterSetDetector
    {
        public ResultCharacterSet Detect(string text)
        {
            var isGSMOnly = CharacterSetRegex.Expression.IsMatch(text);
            return isGSMOnly ? ResultCharacterSet.GSM : ResultCharacterSet.Unicode;
        }
    }

    public class RegexCharacterSetDetectorUsingCompiledOption : ICharacterSetDetector
    {
        public ResultCharacterSet Detect(string text)
        {
            var isGSMOnly = CharacterSetRegexCompiled.Expression.IsMatch(text);
            return isGSMOnly ? ResultCharacterSet.GSM : ResultCharacterSet.Unicode;
        }
    }
}