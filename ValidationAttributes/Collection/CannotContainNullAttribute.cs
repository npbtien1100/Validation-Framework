using System.Collections;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value does not contain null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotContainNullAttribute : ValidationAttribute
    {
        #region Public Methods
        protected override string GetDefaultMessage()
        {
            return "Value cannot contain null.";
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
                value.MustBeSubTypeOf(typeof(IEnumerable));

                var iterator = (value as IEnumerable).GetEnumerator();
                var duplicates = new HashSet<object>();

                while (iterator.MoveNext())
                {
                    if (iterator.Current == null)
                    {
                        return false;
                    }
                }

                return true;
            }
        }

        #endregion Public Methods
    }
}
