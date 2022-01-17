namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is equal to null.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeNullAttribute : ValidationAttribute
    {
        protected override string GetDefaultMessage()
        {
            return "Value must be null.";
        }

        public override bool IsValid(object value)
        {
            return value == null ||
                   value == DBNull.Value;
        }
    }
}
