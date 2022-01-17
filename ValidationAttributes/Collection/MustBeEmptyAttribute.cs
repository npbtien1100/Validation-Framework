using System.Collections;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is empty.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeEmptyAttribute : ValidationAttribute
    {

        protected override string GetDefaultMessage()
        {
            return "Value must be empty.";
        }

        #region Public Methods
        public override bool IsValid(object value)
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return true;
            }
            else
            {
                value.MustBeSubTypeOf(typeof(IEnumerable));

                return !(value as IEnumerable).GetEnumerator().MoveNext();
            }
        }
        #endregion Public Methods
    }
}
