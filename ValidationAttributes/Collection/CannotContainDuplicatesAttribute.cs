using System.Collections;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value does not contain duplicates.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotContainDuplicatesAttribute : ValidationAttribute
    {
        #region Public Methods
        protected override string GetDefaultMessage()
        {
            return "Value cannot contain duplicates.";
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
                    if (duplicates.Contains(iterator.Current))
                    {
                        return false;
                    }
                    else
                    {
                        duplicates.Add(iterator.Current);
                    }
                }

                return true;
            }
        }

        #endregion Public Methods
    }
}
