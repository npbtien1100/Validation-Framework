using System.Globalization;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is a valid date string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeValidDateAttribute : ValidationAttribute
    {
        #region Public Properties
        public string DateFormat
        {
            get;
            set;
        }

        #endregion Public Properties

        protected override string GetDefaultMessage()
        {
            return "Value must be a valid date.";
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

                if (this.DateFormat != null)
                {
                    if (DateTime.TryParseExact(value as string, this.DateFormat, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dateValue))
                    {
                        return true;
                    }
                }
                else
                {
                    if (DateTime.TryParse(value as string, CultureInfo.CurrentCulture, DateTimeStyles.None, out DateTime dateValue))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
