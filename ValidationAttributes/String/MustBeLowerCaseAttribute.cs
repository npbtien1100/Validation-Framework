using System.Globalization;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is a lower case string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeLowerCaseAttribute : ValidationAttribute
    {
        protected override string GetDefaultMessage()
        {
            return "Value must be lower case.";
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

                return value as string == (value as string).ToLower(CultureInfo.CurrentCulture);
            }
        }
    }
}
