namespace Scrummy.Domain.Core.Validators
{
    public class ReferenceValidator
    {
        public static bool ValidateReferenceIsNotNull<T>(T reference) => reference != null;
    }
}
