namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not of specified type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeTypeOfAttribute : ValidationAttribute
    {
        public CannotBeTypeOfAttribute(Type type)
            : this()
        {
            type.CannotBeNull();

            this.Type = type;
        }
        private CannotBeTypeOfAttribute()
        {
        }

        #region Public Properties
        public Type Type
        {
            get;
        }

        #endregion Public Properties

        protected override string GetDefaultMessage()
        {
            return "Value cannot be type of {0}.";
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
                return value.GetType() != this.Type;
            }
        }

        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.Type.Name };
        }
    }
}
