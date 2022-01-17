using System.Collections;

namespace ValidationFramework
{
    public static class HelperExtensions
    {
        public static bool IsNull<T>(this T value) where T : class
        {
            return value == null;
        }
        public static T CannotBeNull<T>(this T value, Action errorHandler = null) where T : class
        {
            if (value.IsNull())
            {
                if (errorHandler.IsNull())
                {
                    throw new ArgumentNullException(null, "Value cannot be null.");
                }
                else
                {
                    errorHandler();
                }
            }

            return value;
        }
        public static bool IsSubTypeOf<T>(this T value, Type type)
        {
            type.CannotBeNull();

            if (!typeof(T).IsValueType)
            {
                if (value != null)
                {
                    return type.IsAssignableFrom(value.GetType());
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return type.IsAssignableFrom(value.GetType());
            }
        }
        public static T MustBeSubTypeOf<T>(this T value, Type type, Action errorHandler = null)
        {
            if (value.IsNotSubTypeOf(type))
            {
                if (errorHandler.IsNull())
                {
                    throw new ArgumentException($"Value must be subtype of {type.Name}.");
                }
                else
                {
                    errorHandler();
                }
            }

            return value;
        }
        public static bool IsNotSubTypeOf<T>(this T value, Type type)
        {
            return !value.IsSubTypeOf(type);
        }
        public static bool IsLessThanOrEqualTo<T>(this T value, T maxValue) where T : IComparable
        {
            if (!typeof(T).IsValueType)
            {
                ((object)value).CannotBeNull();
                ((object)maxValue).CannotBeNull();
            }

            return value.CompareTo(maxValue) <= 0;
        }
        public static T CannotBeLessThanOrEqualTo<T>(this T value, T maxValue, Action errorHandler = null)
            where T : IComparable
        {
            if (value.IsLessThanOrEqualTo(maxValue))
            {
                if (errorHandler.IsNull())
                {
                    throw new ArgumentException($"Value cannot be less than or equal to {maxValue}.");
                }
                else
                {
                    errorHandler();
                }
            }

            return value;
        }
        public static T MustBeTypeOf<T>(this T value, Type type, Action errorHandler = null)
        {
            if (!value.IsTypeOf(type))
            {
                if (errorHandler.IsNull())
                {
                    throw new ArgumentException($"Value must be of type {type.Name}.");
                }
                else
                {
                    errorHandler();
                }
            }

            return value;
        }
        public static bool IsTypeOf<T>(this T value, Type type)
        {
            type.CannotBeNull();

            if (typeof(T).IsValueType)
            {
                return value.GetType() == type;
            }
            else
            {
                if (value == null)
                {
                    return false;
                }
                else
                {
                    return value.GetType() == type;
                }
            }
        }
        public static T CannotBeNullOrEmpty<T>(this T value, Action errorHandler = null)
            where T : IEnumerable
        {
            if (value.IsNullOrEmpty())
            {
                if (errorHandler.IsNull())
                {
                    throw new ArgumentException("Value cannot be null or empty.");
                }
                else
                {
                    errorHandler();
                }
            }

            return value;
        }
        public static bool IsNullOrEmpty<T>(this T value) where T : IEnumerable
        {
            if (value == null)
            {
                return true;
            }
            else
            {
                return !value.GetEnumerator().MoveNext();
            }
        }
        public static T MustBeBetween<T>(this T value, T minValue, T maxValue, bool inclusive = true, Action errorHandler = null) where T : IComparable
        {
            if (!value.IsBetween(minValue, maxValue, inclusive))
            {
                if (errorHandler.IsNull())
                {
                    throw new ArgumentException($"Value must be between {minValue} and {maxValue}.");
                }
                else
                {
                    errorHandler();
                }
            }

            return value;
        }
        public static bool IsBetween<T>(this T value, T minValue, T maxValue, bool inclusive = true) where T : IComparable
        {
            if (!typeof(T).IsValueType)
            {
                ((object)value).CannotBeNull();
                ((object)minValue).CannotBeNull();
                ((object)maxValue).CannotBeNull();
            }

            if (!minValue.IsLessThanOrEqualTo(maxValue))
            {
                throw new ArgumentException($"Min value must be less than or equal to max value (min: {minValue}, max: {maxValue}).");
            }

            if (inclusive)
            {
                return value.IsGreaterThanOrEqualTo(minValue) &&
                       value.IsLessThanOrEqualTo(maxValue);
            }
            else
            {
                return value.IsGreaterThan(minValue) &&
                       value.IsLessThan(maxValue);
            }
        }
        public static bool IsGreaterThanOrEqualTo<T>(this T value, T minValue) where T : IComparable
        {
            if (!typeof(T).IsValueType)
            {
                ((object)value).CannotBeNull();
                ((object)minValue).CannotBeNull();
            }

            return value.CompareTo(minValue) >= 0;
        }
        public static bool IsLessThan<T>(this T value, T maxValue) where T : IComparable
        {
            if (!typeof(T).IsValueType)
            {
                ((object)value).CannotBeNull();
                ((object)maxValue).CannotBeNull();
            }

            return value.CompareTo(maxValue) < 0;
        }
        public static bool IsGreaterThan<T>(this T value, T minValue) where T : IComparable
        {
            if (!typeof(T).IsValueType)
            {
                ((object)value).CannotBeNull();
                ((object)minValue).CannotBeNull();
            }

            return value.CompareTo(minValue) > 0;
        }
        public static T MustBeGreaterThanOrEqualTo<T>(this T value, T minValue, Action errorHandler = null) where T : IComparable
        {
            if (!value.IsGreaterThanOrEqualTo(minValue))
            {
                if (errorHandler.IsNull())
                {
                    throw new ArgumentException($"Value must be greater than or equal to {minValue}.");
                }
                else
                {
                    errorHandler();
                }
            }

            return value;
        }
        public static T MustBeLessThanOrEqualTo<T>(this T value, T maxValue, Action errorHandler = null) where T : IComparable
        {
            if (!value.IsLessThanOrEqualTo(maxValue))
            {
                if (errorHandler.IsNull())
                {
                    throw new ArgumentException($"Value must be less than or equal to {maxValue}.");
                }
                else
                {
                    errorHandler();
                }
            }

            return value;
        }
    }
}