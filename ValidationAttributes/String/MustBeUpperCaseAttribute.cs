using System.Globalization;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is an upper case string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeUpperCaseAttribute : ValidationAttribute
    {
        protected override string GetDefaultMessage()
        {
            return "Value must be upper case.";
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

                return value as string == (value as string).ToUpper(CultureInfo.CurrentCulture);
            }
        }
    }
}
