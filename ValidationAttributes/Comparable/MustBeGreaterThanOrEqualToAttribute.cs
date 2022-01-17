namespace ValidationFramework
{
    /// <summary>
    /// Validation attrubite demanding the value is greater than or equal to the specified limit.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeGreaterThanOrEqualToAttribute : ValidationAttribute
    {
        public MustBeGreaterThanOrEqualToAttribute(object minValue)
            : this()
        {
            this.MinValue = (IComparable)minValue;
        }

        private MustBeGreaterThanOrEqualToAttribute()
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
            return "Value must be greater than or equal to {0}.";
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

                return ((IComparable)value).CompareTo(this.MinValue) >= 0;
            }
        }


        #region Protected Methods
        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.MinValue };
        }
        #endregion Protected Methods
    }
}
