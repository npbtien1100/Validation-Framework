namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is today's date.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeTodayAttribute : ValidationAttribute
    {
        protected override string GetDefaultMessage()
        {
            return "Value must be today.";
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

                return dateValue.Date == DateTime.Today.Date;
            }
        }
    }
}
