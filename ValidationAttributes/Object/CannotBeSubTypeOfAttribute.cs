namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is not subtype of specified type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeSubTypeOfAttribute : ValidationAttribute
    {
        public CannotBeSubTypeOfAttribute(Type type)
                    : this()
        {
            this.Type = type;
        }

        private CannotBeSubTypeOfAttribute()
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
            return "Value cannot be sub-type of {0}.";
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
                return !value.IsSubTypeOf(this.Type);
            }
        }

        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.Type.Name };
        }
    }
}
