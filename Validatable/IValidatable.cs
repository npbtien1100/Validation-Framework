namespace ValidationFramework
{
    /// <summary>
    /// The interface that a class must implement in order to enable property validation.
    /// </summary>
    public interface IValidatable
    {
        #region Public Methods
        ValidationResult Validate(string propertyName, object propertyValue);

        #endregion Public Methods
    }
}
