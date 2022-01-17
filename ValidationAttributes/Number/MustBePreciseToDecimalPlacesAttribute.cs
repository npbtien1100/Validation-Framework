namespace ValidationFramework
{
    /// <summary>
    /// The validation attribute demanding the value is precise to the specified number of decimal places.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class MustBePreciseToDecimalPlacesAttribute : ValidationAttribute
    {
        public MustBePreciseToDecimalPlacesAttribute(int decimalPlaces)
            : this()
        {
            decimalPlaces.MustBeGreaterThanOrEqualTo(0);

            this.DecimalPlaces = decimalPlaces;
        }

        private MustBePreciseToDecimalPlacesAttribute()
        {
        }

        #region Public Properties
        public int DecimalPlaces
        {
            get;
            private set;
        }

        #endregion Public Properties

        protected override string GetDefaultMessage()
        {
            return "Value must be precise to {0} decimal places.";
        }

        public override bool IsValid(object value)
        {
            Type valueType;

            if (value == null ||
                value == DBNull.Value)
            {
                return true;
            }
            else
            {
                this.DecimalPlaces.MustBeGreaterThanOrEqualTo(0);

                valueType = value.GetType();

                if (valueType == typeof(decimal))
                {
                    decimal coefficient = (decimal)Math.Pow(10, this.DecimalPlaces);

                    return (decimal)value == Math.Round((decimal)value * coefficient) / coefficient;
                }
                else if (valueType == typeof(double))
                {
                    double coefficient = Math.Pow(10, this.DecimalPlaces);

                    return (double)value == Math.Round((double)value * coefficient) / coefficient;
                }
                else if (valueType == typeof(float))
                {
                    float coefficient = (float)Math.Pow(10, this.DecimalPlaces);

                    return (float)value == (float)(Math.Round((float)value * coefficient) / coefficient);
                }
                else if (valueType == typeof(byte) ||
                         valueType == typeof(short) ||
                         valueType == typeof(int) ||
                         valueType == typeof(long) ||
                         valueType == typeof(ushort) ||
                         valueType == typeof(uint) ||
                         valueType == typeof(ulong))
                {
                    return true;
                }
                else
                {
                    throw new ArgumentException("Invalid value type.");
                }
            }
        }

        protected override IEnumerable<object> GetParameters()
        {
            return new object[] { this.DecimalPlaces };
        }
    }
}
