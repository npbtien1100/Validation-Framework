namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is subtype of specified type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeSubTypeOfAttribute : ValidationAttribute
    {
        #region Public Constructors
        public MustBeSubTypeOfAttribute(Type type)
            : this()
        {
            this.Type = type;
        }

        #endregion Public Constructors

        #region Private Constructors
        private MustBeSubTypeOfAttribute()
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
            return "Value must be sub-type of {0}.";
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
                return value.IsSubTypeOf(this.Type);
            }
        }

        #endregion Public Methods

        #region Protected Methods
        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.Type.Name };
        }

        #endregion Protected Methods
    }
}
