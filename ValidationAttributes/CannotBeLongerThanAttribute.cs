using System.Collections;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not longer than the specified limit.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeLongerThanAttribute : ValidationAttribute
    {
        #region Public Constructors
        public CannotBeLongerThanAttribute(int maxLength)
            : this()
        {
            maxLength.CannotBeLessThanOrEqualTo(0);

            this.MaxLength = maxLength;

            this.MessageParameters = new List<object> { maxLength };
        }

        #endregion Public Constructors

        #region Private Constructors
        private CannotBeLongerThanAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties
        public int MaxLength
        {
            get;
        }

        #endregion Public Properties

        #region Public Methods
        protected override string GetDefaultMessage()
        {
            return "Value cannot have more than or equal to {0} items.";
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
                value.MustBeSubTypeOf(typeof(IEnumerable));

                var iterator = (value as IEnumerable).GetEnumerator();
                var count = 0;

                while (iterator.MoveNext())
                {
                    count++;
                }

                return count <= this.MaxLength;
            }
        }

        #endregion Public Methods

        #region Protected Methods
        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.MaxLength };
        }

        #endregion Protected Methods
    }
}
