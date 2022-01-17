using System.Globalization;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is a title case string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeTitleCaseAttribute : ValidationAttribute
    {
        protected override string GetDefaultMessage()
        {
            return "Value must be title case.";
        }

        public override bool IsValid(object value)
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return true;
            }
            else
            {
                value.MustBeTypeOf(typeof(string));

                return value as string == CultureInfo.CurrentCulture.TextInfo.ToTitleCase((value as string).ToLower(CultureInfo.CurrentCulture));
            }
        }
    }
}
