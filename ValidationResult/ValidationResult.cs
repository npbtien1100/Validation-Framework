

namespace ValidationFramework
{
    public abstract class ValidationResult
    {
        protected List<ValidationFailure> failures;

        public ValidationResult()
        {
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

        public abstract bool IsLeafResult();

        // public abstract ValidationResult GetResultFor(string propertyName);

        // public abstract List<ValidationResult> GetAllResults();

        public abstract Dictionary<string, List<ValidationFailure>> GetAllFailuresFor(string propertyName);

        public abstract Dictionary<string, List<ValidationFailure>> GetAllFailures();

        #endregion Public Methods
    }
}
