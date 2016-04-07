using System;

namespace CharacterSetDetector
{
    // http://stackoverflow.com/questions/4616685/how-to-generate-a-random-string-and-specify-the-length-you-want-or-better-gene
    public static class RandomStrings
    {
        public const string AllowedGSMChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#@$^*()";
        public const string AllowedUnicodeChars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz#@$^*()Ú";

        public static string GSMString(int length)
        {
            Random rng = new Random();

            char[] chars = new char[length];
            int setLength = AllowedGSMChars.Length;

            for (int i = 0; i < length; ++i)
            {
                chars[i] = AllowedGSMChars[rng.Next(setLength)];
            }

            return new string(chars, 0, length);
        }

        public static string UnicodeString(int length)
        {
            var random = new Random();
            var unicodeString = new char[length];
            var setLength = AllowedGSMChars.Length;
            var anyUnicode = false;

            for (var i = 0; i < length; ++i)
            {
                var addUnicodeCharacter = random.Next(60) == 1;
              
                if (addUnicodeCharacter || (!anyUnicode && i == length - 1))
                {
                    unicodeString[i] = 'Ú';
                    anyUnicode = true;
                }
                else
                {
                    unicodeString[i] = AllowedGSMChars[random.Next(setLength)];
                }
            }

            return new string(unicodeString, 0, length);
        }
    }
}