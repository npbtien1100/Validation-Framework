namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is a date in the future.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeInTheFutureAttribute : ValidationAttribute
    {
        protected override string GetDefaultMessage()
        {
            return "Value must be in the future.";
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
                value.MustBeTypeOf(typeof(DateTime));

                var dateValue = (DateTime)value;

                return dateValue > DateTime.Now;
            }
        }
    }
}
