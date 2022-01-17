﻿namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not less than the specified limit.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeLessThanAttribute : ValidationAttribute
    {
        public CannotBeLessThanAttribute(object minValue)
            : this()
        {
            this.MinValue = (IComparable)minValue;
        }

        #region Private Constructors
        private CannotBeLessThanAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties
        public IComparable MinValue
        {
            get;
            private set;
        }

        #endregion Public Properties
        protected override string GetDefaultMessage()
        {
            return "Value cannot be less than {0}.";
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
