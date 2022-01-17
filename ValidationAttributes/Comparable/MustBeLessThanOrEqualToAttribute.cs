﻿namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is less than the specified limit.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeLessThanOrEqualToAttribute : ValidationAttribute
    {
        public MustBeLessThanOrEqualToAttribute(object maxValue)
            : this()
        {
            this.MaxValue = (IComparable)maxValue;
        }

        private MustBeLessThanOrEqualToAttribute()
        {
        }

        #region Public Properties
        public IComparable MaxValue
        {
            get;
            private set;
        }

        #endregion Public Properties

        protected override string GetDefaultMessage()
        {
            return "Value must be less than or equal to {0}.";
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

        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.MaxValue };
        }
    }
}
