using System.Collections;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value contains duplicates.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustContainDuplicatesAttribute : ValidationAttribute
    {
        protected override string GetDefaultMessage()
        {
            return "Value must contain duplicates.";
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
                        return true;
                    }
                    else
                    {
                        duplicates.Add(iterator.Current);
                    }
                }

                return false;
            }
        }

    }
}
