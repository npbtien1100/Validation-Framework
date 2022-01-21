namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute declare for nested validation attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class NestedValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            return true;
        }

        protected override string GetDefaultMessage()
        {
            return "Value must be a nested validation attribute!";
        }
    }
}
