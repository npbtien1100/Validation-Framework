namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not greater than the specified limit.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeGreaterThanAttribute : ValidationAttribute
    {
        public CannotBeGreaterThanAttribute(object minValue)
            : this()
        {
            this.MaxValue = (IComparable)minValue;
        }

        #region Private Constructors
        private CannotBeGreaterThanAttribute()
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

        #region Public Methods
        protected override string GetDefaultMessage()
        {
            return "Value cannot be greater than {0}.";
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

                return ((IComparable)value).CompareTo(this.MaxValue) <= 0;

            }
        }

        #endregion Public Methods

        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.MaxValue };
        }

    }
}
