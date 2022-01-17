using System.Collections;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value contains null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustContainNullAttribute : ValidationAttribute
    {
        protected override string GetDefaultMessage()
        {
            return "Value must contain null.";
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
                        return true;
                    }
                }

                return false;
            }
        }
    }
}
