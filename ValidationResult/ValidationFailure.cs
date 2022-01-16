namespace ValidationFramework
{
    public class ValidationFailure
    {
        public string? ErrorMessage
        {
            get;
            set;
        }
        public ValidationLevel ValidationLevel
        {
            get;
            set;
        }
        public ValidationFailure(string errorMessage, ValidationLevel validationLevel = ValidationLevel.Error)
        {
            ErrorMessage = errorMessage;
            ValidationLevel = validationLevel;
        }
    }
}