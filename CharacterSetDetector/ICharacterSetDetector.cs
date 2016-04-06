namespace CharacterSetDetector
{
    public interface ICharacterSetDetector
    {
        ResultCharacterSet Detect(string text);
    }
}