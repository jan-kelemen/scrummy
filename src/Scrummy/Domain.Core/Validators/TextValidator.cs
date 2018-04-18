namespace Scrummy.Domain.Core.Validators
{
    public class TextValidator
    {
        public static bool ValidateThatTextCanRepresentIdentity(string identity) => ValidateThatContentIsBetweenSpecifiedLength(identity, 1, int.MaxValue);

        public static bool ValidateThatContentIsBetweenSpecifiedLength(string text, int minimumLength = 0, int maximumLength = int.MaxValue)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return false; 
            }

            return text.Length >= minimumLength && text.Length <= maximumLength;
        }
    }
}
