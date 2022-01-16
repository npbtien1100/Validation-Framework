namespace ValidationFramework
{
    /// <summary>
    /// The interface that a class must implement in order to enable property validation.
    /// </summary>
    public interface IValidatable
    {
        #region Public Methods
        /// <summary>
        /// Validates the specified property name for the specified validation context.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="propertyValue">The property value.</param>
        /// <returns>
        /// The list of validation messages.
        /// </returns>
        ValidationResult Validate(string propertyName, object propertyValue);

        #endregion Public Methods
    }
}
