using System.Globalization;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is a valid time span string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeValidTimeSpanAttribute : ValidationAttribute
    {
        #region Public Properties
        public string TimeSpanFormat
        {
            get;
            set;
        }

        #endregion Public Properties
        protected override string GetDefaultMessage()
        {
            return "Value must be a valid time span.";
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

                if (this.TimeSpanFormat != null)
                {
                    if (TimeSpan.TryParseExact(value as string, this.TimeSpanFormat, CultureInfo.CurrentCulture, TimeSpanStyles.None, out TimeSpan timeSpan))
                    {
                        return true;
                    }
                }
                else
                {
                    if (TimeSpan.TryParse(value as string, CultureInfo.CurrentCulture, out TimeSpan timeSpan))
                    {
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
