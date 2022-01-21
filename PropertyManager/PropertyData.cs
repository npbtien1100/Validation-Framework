using System.Reflection;


namespace ValidationFramework
{
    /// <summary>
    /// Property data.
    /// </summary>
    public class PropertyData
    {
        public PropertyData(PropertyInfo propertyInfo, IEnumerable<ValidationAttribute> validationAttributes)
            : this()
        {
            propertyInfo.CannotBeNull();
            validationAttributes.CannotBeNull();

            this.PropertyInfo = propertyInfo;
            this.ValidationAttributes = validationAttributes;
        }

        private PropertyData()
        {
        }

        #region Public Properties
        public PropertyInfo PropertyInfo
        {
            get;
            private set;
        }

        public IEnumerable<ValidationAttribute> ValidationAttributes
        {
            get;
            private set;
        }

        #endregion Public Properties
        public bool ContainsNestedValidationAttribute()
        {
            return ValidationAttributes.Any(x => x.GetType().Name.Equals("NestedValidationAttribute"));
        }
    }
}
