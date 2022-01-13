using System.Reflection;

namespace ValidationFramework
{
    /// <summary>
    /// Reflection extension methods.
    /// </summary>
    public class PropertyManager
    {
        private static PropertyManager? instance;
        private Dictionary<Type, Dictionary<string, PropertyData>> typeProperties = new Dictionary<Type, Dictionary<string, PropertyData>>();

        private PropertyManager()
        {
        }
        public static PropertyManager getInstance()
        {
            if (instance == null)
                instance = new PropertyManager();
            return instance;
        }
        /// <summary>
        /// Gets the properties for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>The property information.</returns>
        public Dictionary<string, PropertyData> GetProperties(Type type)
        {
            // TODO: should we include fields as well?
            BindingFlags bindings = BindingFlags.Public | BindingFlags.Instance;

            if (!typeProperties.TryGetValue(type, out Dictionary<string, PropertyData> properties))
            {
                properties = new Dictionary<string, PropertyData>();

                // get property information
                foreach (var propertyInfo in type.GetProperties(bindings))
                {
                    properties.Add(propertyInfo.Name, new PropertyData(propertyInfo, propertyInfo.GetCustomAttributes<ValidationAttribute>(true).Cast<ValidationAttribute>().ToList()));
                }

                // cache property information
                typeProperties.Add(type, properties);
            }

            return properties;
        }
    }
}
