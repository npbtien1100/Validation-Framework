namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding value is not between specified limits.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class CannotBeBetweenAttribute : ValidationAttribute
    {

        public CannotBeBetweenAttribute(object minValue, object maxValue, bool inclusive = true)
            : this()
        {
            this.MinValue = (IComparable)minValue;
            this.MaxValue = (IComparable)maxValue;
            this.Inclusive = inclusive;
        }

        private CannotBeBetweenAttribute()
        {
        }

        #region Public Properties
        public bool Inclusive
        {
            get;
        }

        public IComparable MaxValue
        {
            get;
        }

        public IComparable MinValue
        {
            get;
        }

        #endregion Public Properties

        protected override string GetDefaultMessage()
        {
            return "Value cannot be between {0} and {1}.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                this.MinValue.CannotBeNull();
                this.MaxValue.CannotBeNull();
                this.MinValue.MustBeTypeOf(this.MaxValue.GetType());
                this.MinValue.MustBeLessThanOrEqualTo(this.MaxValue);


                value.MustBeTypeOf(this.MinValue.GetType());
                value.MustBeTypeOf(this.MaxValue.GetType());

                return !(value as IComparable).IsBetween(this.MinValue, this.MaxValue, this.Inclusive);
            }
        }

        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.MinValue, this.MaxValue };
        }

    }
}
