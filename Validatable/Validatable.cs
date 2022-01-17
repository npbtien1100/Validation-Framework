namespace ValidationFramework
{
    /// <summary>
    /// The validatable base class.
    /// </summary>
    public abstract class Validatable
    {
        #region Public Methods
        public virtual ValidationResult Validate(string propertyName, object propertyValue)
        {
            return this.ValidateAttributes(propertyName, propertyValue);
        }
        public bool IsValid(string propertyName)
        {
            this.CannotBeNull();

            return this.Validate(propertyName).IsValid();
        }

        public bool IsValid()
        {
            this.CannotBeNull();

            return this.Validate().IsValid();
        }

        public ValidationResult Validate(string propertyName)
        {
            this.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            ValidationResult result = new CompositeResult();
            object propertyValue;

            // get property value
            PropertyManager propertyManager = PropertyManager.getInstance();
            var properties = propertyManager.GetProperties(this.GetType());

            if (properties.TryGetValue(propertyName, out PropertyData propertyData))
            {
                if (propertyData.PropertyInfo.CanRead &&
                    propertyData.PropertyInfo.CanWrite)
                {
                    propertyValue = propertyData.PropertyInfo.GetValue(this);

                    result = this.Validate(propertyName, propertyValue);
                }
            }

            return result;
        }

        public ValidationResult Validate()
        {
            this.CannotBeNull();

            CompositeResult messages = new CompositeResult();

            PropertyManager propertyManager = PropertyManager.getInstance();
            var propertyNames = propertyManager.GetProperties(this.GetType()).Keys;

            foreach (var propertyName in propertyNames)
            {
                messages.AddResult(this.Validate(propertyName));
            }

            return messages;
        }

        protected ValidationResult ValidateAttributes(string propertyName, object propertyValue)
        {
            this.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            List<ValidationFailure> failures = new List<ValidationFailure>();

            PropertyManager propertyManager = PropertyManager.getInstance();
            var validationAttributes = propertyManager.GetProperties(this.GetType())[propertyName].ValidationAttributes;
            bool isValid;

            // perform attribute based validation
            foreach (var validationAttribute in validationAttributes)
            {
                // custom validators might cause exceptions that are hard to find
                try
                {
                    isValid = validationAttribute.IsValid(propertyValue);
                }
                catch (Exception ex)
                {
                    throw new ValidationErrorException("Unhandled validation exception occurred.", validationAttribute.GetType(), this.GetType(), propertyName, ex);
                }

                if (!isValid)
                {
                    var message = validationAttribute.GetMessage();
                    var validationLevel = validationAttribute.ValidationLevel;

                    // value is invalid -> add it to the list
                    failures.Add(new ValidationFailure(message, validationLevel));
                }

            }

            return new LeafResult(propertyName, propertyValue, failures);
        }
        #endregion Public Methods
    }
}
