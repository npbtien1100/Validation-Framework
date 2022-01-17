using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value matches the specified regular expression.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustMatchAttribute : ValidationAttribute
    {
        #region Public Constructors
        public MustMatchAttribute(string regex, RegexOptions regexOptions = RegexOptions.None)
            : this()
        {
            regex.CannotBeNullOrEmpty();

            this.Regex = new Regex(regex, regexOptions);
        }

        #endregion Public Constructors

        #region Private Constructors

        private MustMatchAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties
        public Regex Regex
        {
            get;
        }

        #endregion Public Properties

        #region Public Methods
        protected override string GetDefaultMessage()
        {
            return "Value must match {0}.";
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

                return this.Regex.IsMatch(value as string);
            }
        }

        #endregion Public Methods

        #region Protected Methods
        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.Regex.ToString() };
        }

        #endregion Protected Methods
    }
}
