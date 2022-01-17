namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not equal to the specified value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeEqualToAttribute : ValidationAttribute
    {
        #region Public Constructors

        public CannotBeEqualToAttribute(object value)
            : this()
        {
            this.ComparedValue = value;
        }

        #endregion Public Constructors

        #region Private Constructors
        private CannotBeEqualToAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties
        public object ComparedValue
        {
            get;

            private set;
        }

        #endregion Public Properties

        protected override string GetDefaultMessage()
        {
            return "Value cannot be equal to {0}.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return this.ComparedValue != null;
            }
            else if (value == DBNull.Value)
            {
                return this.ComparedValue != DBNull.Value;
            }
            else
            {
                value.MustBeSubTypeOf(typeof(IComparable));

                if (this.ComparedValue == null)
                {
                    return value != null;
                }
                else if (this.ComparedValue == DBNull.Value)
                {
                    return value != DBNull.Value;
                }
                else
                {
                    value.MustBeTypeOf(this.ComparedValue.GetType());

                    return ((IComparable)value).CompareTo(this.ComparedValue) != 0;
                }
            }
        }

        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.ComparedValue };
        }
    }
}
