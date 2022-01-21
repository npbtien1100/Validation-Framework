namespace ValidationFramework
{
    public class LeafResult : ValidationResult
    {

        public LeafResult()
        {
            failures = new List<ValidationFailure>();
        }

        public LeafResult(string propertyName, object propertyValue, List<ValidationFailure> failures) : base(propertyName, propertyValue, failures)
        {
        }


        public override Dictionary<string, List<ValidationFailure>> GetAllFailures()
        {
            return new Dictionary<string, List<ValidationFailure>>() { { PropertyName, failures } };
        }

        public override Dictionary<string, List<ValidationFailure>> GetAllFailuresFor(string propertyName)
        {
            if (propertyName.Equals(PropertyName))
                return new Dictionary<string, List<ValidationFailure>>() { { PropertyName, failures } };
            return new Dictionary<string, List<ValidationFailure>>();
        }


        public override bool IsValid()
        {
            return !failures.Any();
        }
    }
}