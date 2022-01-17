using System.Collections;

namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not shorter than the specified limit.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeShorterThanAttribute : ValidationAttribute
    {
        #region Public Constructors
        public CannotBeShorterThanAttribute(int minLength)
            : this()
        {
            minLength.MustBeGreaterThanOrEqualTo(0);

            this.MinLength = minLength;
        }

        #endregion Public Constructors

        #region Private Constructors

        private CannotBeShorterThanAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties
        public int MinLength
        {
            get;
        }

        #endregion Public Properties

        protected override string GetDefaultMessage()
        {
            return "Value cannot have less than or equal to {0} items.";
        }
        #region Public Methods
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

                return count >= this.MinLength;
            }
        }

        #endregion Public Methods

        #region Protected Methods
        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.MinLength };
        }

        #endregion Protected Methods
    }
}
