namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not null or whitespace.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeNullOrWhitespaceAttribute : ValidationAttribute
    {
        protected override string GetDefaultMessage()
        {
            return "Value cannot be null or white space.";
        }

        public override bool IsValid(object value)
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return false;
            }
            else
            {
                value.MustBeTypeOf(typeof(string));

                return !string.IsNullOrWhiteSpace(value as string);
            }
        }
    }
}
