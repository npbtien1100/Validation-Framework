namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value does not belong to the specified set of values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class CannotBeOneOfAttribute : ValidationAttribute
    {
        public CannotBeOneOfAttribute(params object[] set)
            : this()
        {
            set.CannotBeNullOrEmpty();

            this.Set = set;
        }

        private CannotBeOneOfAttribute()
        {
        }

        #region Public Properties
        public IEnumerable<object> Set
        {
            get;
        }

        #endregion Public Properties

        protected override string GetDefaultMessage()
        {
            return "Value cannot be one of: {0}.";
        }

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return !this.Set.Any(x => x == null);
            }
            else if (value == DBNull.Value)
            {
                return !this.Set.Any(x => x == DBNull.Value);
            }
            else
            {
                return !this.Set.Any(x => ((IComparable)value).CompareTo(x) == 0);
            }
        }

        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { string.Join(", ", this.Set.Select(x => x.ToString())) };
        }
    }
}
