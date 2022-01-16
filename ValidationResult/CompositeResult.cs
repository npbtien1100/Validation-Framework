namespace ValidationFramework
{
    public class CompositeResult : ValidationResult
    {
        private Dictionary<string, ValidationResult> errors;
        private List<ValidationFailure> failures;
        public CompositeResult()
        {
            errors = new Dictionary<string, ValidationResult>();
            failures = new List<ValidationFailure>();
        }

        public void AddResult(ValidationResult validationResult)
        {
            if (!errors.ContainsKey(validationResult.PropertyName))
            {
                errors.Add(validationResult.PropertyName, validationResult);
            }
            errors[validationResult.PropertyName] = validationResult;
        }

        public void RemoveResult(string propertyName)
        {
            if (errors.ContainsKey(propertyName))
                errors.Remove(propertyName);
        }
        public override Dictionary<string, List<ValidationFailure>> GetAllFailures()
        {
            Dictionary<string, List<ValidationFailure>> result = new Dictionary<string, List<ValidationFailure>>();
            if (PropertyName != null)
                result.Add(PropertyName, failures);

            foreach (ValidationResult error in errors.Values)
            {
                Dictionary<string, List<ValidationFailure>> dict = error.GetAllFailures();
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
            if (errors.ContainsKey(propertyName))
            {
                Dictionary<string, List<ValidationFailure>> result = new Dictionary<string, List<ValidationFailure>>();

                Dictionary<string, List<ValidationFailure>> dict = errors[propertyName].GetAllFailures();
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

        // public override List<ValidationResult> GetAllResults()
        // {
        //     if (errors.Values.Any())
        //         return errors.Values.ToList();
        //     return new List<ValidationResult>();
        // }

        // public override ValidationResult GetResultFor(string propertyName)
        // {
        //     if (errors.ContainsKey(propertyName))
        //         return errors[propertyName];
        //     return null;
        // }

        public override bool IsLeafResult()
        {
            return false;
        }

        public override bool IsValid()
        {
            foreach (ValidationResult result in errors.Values)
            {
                if (!result.IsValid())
                    return false;
            }
            return true;
        }
    }
}