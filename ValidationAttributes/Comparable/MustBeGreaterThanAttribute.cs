namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is greater than the specified limit.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeGreaterThanAttribute : ValidationAttribute
    {
        public MustBeGreaterThanAttribute(object minValue)
            : this()
        {
            this.MinValue = (IComparable)minValue;
        }

        private MustBeGreaterThanAttribute()
        {
        }
        #region Public Properties
        public IComparable MinValue
        {
            get;
            private set;
        }

        #endregion Public Properties

        protected override string GetDefaultMessage()
        {
            return "Value must be greater than {0}.";
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
                this.MinValue.CannotBeNull();

                value.MustBeTypeOf(this.MinValue.GetType());

                return ((IComparable)value).CompareTo(this.MinValue) > 0;
            }
        }

        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.MinValue };
        }
    }
}
