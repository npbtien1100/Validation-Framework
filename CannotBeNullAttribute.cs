using System;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not equal to null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeNullAttribute : ValidationAttribute
    {
        #region Public Methods
        protected override string GetDefaultMessage()
        {
            return "Value cannot be null.";
        }

        public override bool IsValid(object value)
        {
            return value != null &&
                   value != DBNull.Value;
        }

        #endregion Public Methods
    }
}
