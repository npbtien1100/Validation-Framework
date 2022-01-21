

namespace ValidationFramework
{
    public abstract class ValidationResult
    {
        protected List<ValidationFailure> failures;

        public ValidationResult()
        {
        }
        public ValidationResult(string propertyName, object propertyValue, List<ValidationFailure> failures)
        {
            PropertyName = propertyName;
            AttemptedValue = propertyValue;
            this.failures = failures;
        }
        #region Public Properties
        public string PropertyName
        {
            get;
            set;
        }
        public object AttemptedValue
        {
            get;
            set;
        }
        #endregion Public Properties

        #region Public Methods

        public abstract bool IsValid();

        public abstract Dictionary<string, List<ValidationFailure>> GetAllFailuresFor(string propertyName);

        public abstract Dictionary<string, List<ValidationFailure>> GetAllFailures();

        #endregion Public Methods
    }
}
