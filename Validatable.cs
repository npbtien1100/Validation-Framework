namespace ValidationFramework
{
    /// <summary>
    /// The validatable base class.
    /// </summary>
    /// <seealso cref="ValidationFramework.IValidatable" />
    public abstract class Validatable : IValidatable
    {
        #region Public Methods

        /// <summary>
        /// Gets the list of currently active validation contexts.
        /// </summary>
        /// <returns>
        /// The list of currently active validation context.
        /// </returns>
        // public virtual IEnumerable<string> GetActiveValidationContexts()
        // {
        //     return Array.Empty<string>();
        // }

        /// <summary>
        /// Validates the specified property name.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// The list of validation messages.
        /// </returns>
        public virtual ValidationMessageCollection Validate(string propertyName, object propertyValue)
        {
            return this.ValidateAttributes(propertyName, propertyValue);
        }
        public bool IsValid(string propertyName)
        {
            this.CannotBeNull();

            return !this.Validate(propertyName).Any(x => x.ValidationLevel == ValidationLevel.Error);
        }

        /// <summary>
        /// Checks if the specified specified validation source is valid (has no invalid properties).
        /// </summary>
        /// <param name="validationSource">The validation source.</param>
        /// <returns>
        ///   <c>true</c> if the specified validation source is valid; otherwise, <c>false</c>.
        /// </returns>
        public bool IsValid()
        {
            this.CannotBeNull();

            return !this.Validate().Any(x => x.ValidationLevel == ValidationLevel.Error);
        }

        /// <summary>
        /// Validates the specified property of the specified validation source.
        /// </summary>
        /// <param name="validationSource">The validation source.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns>The collection of validation mesasges.</returns>
        public ValidationMessageCollection Validate(string propertyName)
        {
            this.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            ValidationMessageCollection messages = new ValidationMessageCollection();
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

                    messages.AddRange(this.Validate(propertyName, propertyValue));
                }
            }

            return messages;
        }

        /// <summary>
        /// Validates the the specified validation source.
        /// </summary>
        /// <param name="validationSource">The validation source.</param>
        /// <returns>
        /// The collection of validation mesasges.
        /// </returns>
        public ValidationMessageCollection Validate()
        {
            this.CannotBeNull();

            ValidationMessageCollection messages = new ValidationMessageCollection();

            PropertyManager propertyManager = PropertyManager.getInstance();
            var propertyNames = propertyManager.GetProperties(this.GetType()).Keys;

            foreach (var propertyName in propertyNames)
            {
                messages.AddRange(this.Validate(propertyName));
            }

            return messages;
        }

        /// <summary>
        /// Validates the specified property of the specified validation source for the specified property value in specified validation context by using validation attributes.
        /// </summary>
        /// <param name="validationSource">The validation source.</param>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        /// The collection of validation mesasges.
        /// </returns>
        protected ValidationMessageCollection ValidateAttributes(string propertyName, object propertyValue)
        {
            this.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            ValidationMessageCollection messages = new ValidationMessageCollection();

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
                    messages.Add(new ValidationMessage(message, this, propertyName, validationLevel));
                }

            }

            return messages;
        }
        #endregion Public Methods
    }
}
