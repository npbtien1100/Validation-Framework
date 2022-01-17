namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is a valid URI.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBeValidUriAttribute : ValidationAttribute
    {
        #region Public Properties
        public bool HostOnly
        {
            get;
            set;
        }

        public IEnumerable<string> ValidSchemas
        {
            get;
            set;
        }

        #endregion Public Properties

        protected override string GetDefaultMessage()
        {
            return "Value must be a valid URI.";
        }

        public override bool IsValid(object value)
        {
            if (value == null ||
                value == DBNull.Value)
            {
                return true;
            }
            else
            {
                value.MustBeTypeOf(typeof(string));

                string stringValue = (string)value;

                if (Uri.TryCreate(value as string, UriKind.Absolute, out Uri uri))
                {
                    if (this.ValidSchemas != null &&
                        this.ValidSchemas.Count() > 0 &&
                        !this.ValidSchemas.Contains(uri.Scheme))
                    {
                        return false;
                    }

                    if (this.HostOnly)
                    {
                        if (uri.AbsolutePath != "/")
                        {
                            return false;
                        }
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
