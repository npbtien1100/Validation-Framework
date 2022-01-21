namespace ValidationFramework
{
    public class CompositeResult : ValidationResult
    {
        private Dictionary<string, ValidationResult> results;

        public Dictionary<string, ValidationResult> Results { get => results; set => results = value; }

        public CompositeResult()
        {
            results = new Dictionary<string, ValidationResult>();
            failures = new List<ValidationFailure>();
        }

        public CompositeResult(string propertyName, object propertyValue, List<ValidationFailure> failures) : base(propertyName, propertyValue, failures)
        {
            results = new Dictionary<string, ValidationResult>();
        }

        public void AddResult(ValidationResult validationResult)
        {
            if (validationResult != null && validationResult.PropertyName != null)
            {
                if (!results.ContainsKey(validationResult.PropertyName))
                {
                    results.Add(validationResult.PropertyName, validationResult);
                    return;
                }
                results[validationResult.PropertyName] = validationResult;
            }
        }
        public void AddResult(string propertyName, ValidationResult validationResult)
        {
            if (validationResult != null)
            {
                if (!results.ContainsKey(propertyName))
                {
                    results.Add(propertyName, validationResult);
                    return;
                }
                results[propertyName] = validationResult;
            }
        }

        public void RemoveResult(string propertyName)
        {
            if (results.ContainsKey(propertyName))
                results.Remove(propertyName);
        }
        public override Dictionary<string, List<ValidationFailure>> GetAllFailures()
        {
            Dictionary<string, List<ValidationFailure>> result = new Dictionary<string, List<ValidationFailure>>();
            if (PropertyName != null)
                result.Add(PropertyName, failures);

            foreach (ValidationResult res in results.Values)
            {
                Dictionary<string, List<ValidationFailure>> dict = res.GetAllFailures();
                foreach (string key in dict.Keys)
                {
                    string combinKey = PropertyName != null ? PropertyName + "." + key : key;
                    List<ValidationFailure> temp = dict[key];
                    result.Add(combinKey, temp);
                }
            }
            return result;
        }

        public override Dictionary<string, List<ValidationFailure>> GetAllFailuresFor(string propertyName)
        {
            if (results.ContainsKey(propertyName))
            {
                Dictionary<string, List<ValidationFailure>> result = new Dictionary<string, List<ValidationFailure>>();

                Dictionary<string, List<ValidationFailure>> dict = results[propertyName].GetAllFailures();
                foreach (string key in dict.Keys)
                {
                    string combinKey = PropertyName + "." + key;
                    List<ValidationFailure> temp = dict[key];
                    result[combinKey] = temp;
                }
                return result;
            }
            return new Dictionary<string, List<ValidationFailure>>();
        }

        public override bool IsValid()
        {
            if (failures.Any())
                return false;

            foreach (ValidationResult result in results.Values)
            {
                if (!result.IsValid())
                    return false;
            }
            return true;
        }
    }
}