using System;
using System.Collections;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not null or empty.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeNullOrEmptyAttribute : ValidationAttribute
    {
        #region Public Properties

        protected override string GetDefaultMessage()
        {
            return "Value cannot be null or empty.";
        }

        #endregion Public Properties

        #region Public Methods

        public override bool IsValid(object value)
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return false;
            }
            else
            {
                value.MustBeSubTypeOf(typeof(IEnumerable));

                return (value as IEnumerable).GetEnumerator().MoveNext();
            }
        }

        #endregion Public Methods
    }
}
