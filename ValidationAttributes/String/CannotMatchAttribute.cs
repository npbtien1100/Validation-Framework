using System.Text.RegularExpressions;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value does not match the specified regular expression.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotMatchAttribute : ValidationAttribute
    {
        public CannotMatchAttribute(string regex, RegexOptions regexOptions = RegexOptions.None)
            : this()
        {
            regex.CannotBeNullOrEmpty();

            this.Regex = new Regex(regex, regexOptions);
        }

        private CannotMatchAttribute()
        {
        }

        #region Public Properties
        public Regex Regex
        {
            get;
        }

        #endregion Public Properties
        protected override string GetDefaultMessage()
        {
            return "Value cannot match {0}.";
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

                return !this.Regex.IsMatch(value as string);
            }
        }

        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.Regex.ToString() };
        }
    }
}
