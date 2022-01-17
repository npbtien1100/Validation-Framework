namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not greater than or equal to the specified limit.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeGreaterThanOrEqualToAttribute : ValidationAttribute
    {
        public CannotBeGreaterThanOrEqualToAttribute(object maxValue)
            : this()
        {
            this.MaxValue = (IComparable)maxValue;
        }

        #region Private Constructors
        private CannotBeGreaterThanOrEqualToAttribute()
        {
        }
        #endregion Private Constructors

        #region Public Properties

        public IComparable MaxValue
        {
            get;
            private set;
        }

        #endregion Public Properties
        protected override string GetDefaultMessage()
        {
            return "Value cannot be greater than or equal to {0}.";
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
                this.MaxValue.CannotBeNull();

                value.MustBeTypeOf(this.MaxValue.GetType());

                return ((IComparable)value).CompareTo(this.MaxValue) < 0;

            }
        }

        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.MaxValue };
        }
    }
}
