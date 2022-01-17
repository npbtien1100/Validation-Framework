namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is a date without the time component.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeDateAttribute : ValidationAttribute
    {
        protected override string GetDefaultMessage()
        {
            return "Value must be a date.";
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

                return dateValue == dateValue.Date;
            }
        }
    }
}
