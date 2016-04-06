using System.Linq;

namespace CharacterSetDetector
{
    public class CharacterSetDetector : ICharacterSetDetector
    {
        private const string GSM_ALPHABET = "@£$¥èéùìòÇØøÅåΔ_ΦΓΛΩΠΨΣΘΞ^{}\\[~]|€ÆæßÉ!\"#¤%&'()*+,-./0123456789:;<=>?¡ABCDEFGHIJKLMNOPQRSTUVWXYZÄÖÑÜ§¿abcdefghijklmnopqrstuvwxyzäöñüà\u000c\u0020\u000d\u000a";

        public ResultCharacterSet Detect(string text)
        {
            return text.All(GSM_ALPHABET.Contains) ? ResultCharacterSet.GSM : ResultCharacterSet.Unicode;
        }
    }
}
