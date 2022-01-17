namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not equal to null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeNullAttribute : ValidationAttribute
    {

        protected override string GetDefaultMessage()
        {
            return "Value cannot be null.";
        }

        public override bool IsValid(object value)
        {
            return value != null &&
                   value != DBNull.Value;
        }
    }
}
