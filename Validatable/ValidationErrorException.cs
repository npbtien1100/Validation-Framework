namespace ValidationFramework
{
    /// <summary>
    /// The validation error exception thrown when there is an error when executing validation through a validation attribute.
    /// </summary>
    public sealed class ValidationErrorException : Exception
    {
        public ValidationErrorException(string message, Type validationAttributeType, Type validationSourceType, string propertyName, Exception innerException)
            : base(message, innerException)
        {
            message.CannotBeNullOrEmpty();
            innerException.CannotBeNull();
            validationAttributeType.CannotBeNull();
            validationSourceType.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            this.ValidationAttributeType = validationAttributeType;
            this.ValidationSourceType = validationSourceType;
            this.PropertyName = propertyName;
        }

        private ValidationErrorException()
        {
        }
        #region Public Properties

        public string PropertyName
        {
            get;
            private set;
        }

        public Type ValidationAttributeType
        {
            get;
            private set;
        }

        public Type ValidationSourceType
        {
            get;
            private set;
        }

        #endregion Public Properties
    }
}
