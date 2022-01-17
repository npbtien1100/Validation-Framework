using System.Globalization;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is a valid integer number string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeValidIntegerAttribute : ValidationAttribute
    {
        protected override string GetDefaultMessage()
        {
            return "Value must be a valid integer.";
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

                if (int.TryParse(value as string, NumberStyles.Integer, CultureInfo.CurrentCulture, out int integerValue))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
