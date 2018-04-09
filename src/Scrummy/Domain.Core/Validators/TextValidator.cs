namespace Scrummy.Domain.Core.Validators
{
    public class TextValidator
    {
        public static bool CheckIfContentIsBetweenSpecifiedLength(string text, int minimumLength = 0, int maximumLength = 200)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false; 
            }

            return text.Length >= minimumLength && text.Length <= maximumLength;
        }
    }
}
