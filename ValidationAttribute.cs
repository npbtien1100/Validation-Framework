using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace ValidationFramework
{
    /// <summary>
    /// The base class for deriving validation attributes.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public abstract class ValidationAttribute : Attribute
    {
        protected ValidationAttribute()
        {
            // set the defaults
            this.ValidationLevel = ValidationLevel.Error;
        }

        #region Public Properties
        public string Message
        {
            get;
            set;
        }
        protected IEnumerable<object> MessageParameters;
        // {
        //     get
        //     {
        //         return this.GetParameters();
        //     }
        // }
        public ValidationLevel ValidationLevel
        {
            get;
            set;
        }

        #endregion Public Properties

        #region Public Methods

        protected abstract string GetDefaultMessage();


        public abstract bool IsValid(object value);

        public string GetMessage()
        {
            string defaultMessage = GetDefaultMessage();
            defaultMessage.CannotBeNullOrEmpty();

            string message = defaultMessage;

            try
            {
                if (MessageParameters != null &&
                    MessageParameters.Any())
                {
                    message = string.Format(CultureInfo.CurrentCulture, defaultMessage, MessageParameters.ToArray());
                }
            }
            catch (FormatException)
            {
                // unable to format -> keep unformatted message
            }

            return message;
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual IEnumerable<object> GetParameters()
        {
            return new List<object>();
        }

        #endregion Protected Methods
    }
}
