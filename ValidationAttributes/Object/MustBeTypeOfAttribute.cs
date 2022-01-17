namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is of specified type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeTypeOfAttribute : ValidationAttribute
    {
        #region Public Constructors
        public MustBeTypeOfAttribute(Type type)
                    : this()
        {
            this.Type = type;
        }

        #endregion Public Constructors

        #region Private Constructors
        private MustBeTypeOfAttribute()
        {
        }

        #endregion Private Constructors

        #region Public Properties
        public Type Type
        {
            get;
        }

        #endregion Public Properties

        #region Public Methods
        protected override string GetDefaultMessage()
        {
            return "Value must be type of {0}.";
        }

        public override bool IsValid(object value)
        {
            this.Type.CannotBeNull();

            if (value == null ||
                value == DBNull.Value)
            {
                return true;
            }
            else
            {
                return value.GetType() == this.Type;
            }
        }

        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.Type.Name };
        }

        #endregion Public Methods
    }
}
