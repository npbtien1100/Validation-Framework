using System.Globalization;


namespace ValidationFramework
{
    public class ValidationMessage
    {
        #region Public Constructors
        public ValidationMessage(string message, IValidatable validationSource, string propertyName, ValidationLevel validationLevel = ValidationLevel.Error)
          : this()
        {
            validationSource.CannotBeNull();
            propertyName.CannotBeNullOrEmpty();

            this.Message = message;
            this.ValidationLevel = validationLevel;
            this.ValidationSource = validationSource;
            this.PropertyName = propertyName;
        }

        #endregion Public Constructors

        #region Private Constructors
        private ValidationMessage()
        {
        }

        #endregion Private Constructors

        #region Public Properties
        public string Message
        {
            get;
            private set;
        }

        public string PropertyName
        {
            get;
            private set;
        }

        public ValidationLevel ValidationLevel
        {
            get;
            private set;
        }

        public IValidatable ValidationSource
        {
            get;
            private set;
        }

        #endregion Public Properties

        #region Public Methods
        public override string ToString()
        {
            return $"{this.ValidationSource.GetType().Name}.{this.PropertyName} (message: \"{this.Message}\", level: {this.ValidationLevel})";
        }

        #endregion Public Methods

        #region Private Methods
        private static string FormatMessage(string defaultMessage, IEnumerable<object> messageParameters)
        {
            defaultMessage.CannotBeNullOrEmpty();

            string message;

            message = defaultMessage;

            try
            {
                if (messageParameters != null &&
                    messageParameters.Any())
                {
                    message = string.Format(CultureInfo.CurrentCulture, message, messageParameters.ToArray());
                }
            }
            catch (FormatException)
            {
                // unable to format -> keep unformatted message
            }

            return message;
        }

        #endregion Private Methods
    }
}
