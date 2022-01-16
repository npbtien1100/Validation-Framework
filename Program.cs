using ValidationFramework;





Example example = new Example();
example.Name = "Test";

bool isObjectValid = example.IsValid(); // isValid == true
bool isPropertyValid = example.IsValid(nameof(example.Name)); // isValid == true
ValidationResult objectValidationMessages = example.Validate(); // validationMessages.Count == 0
ValidationResult propertyValidationMessages = example.Validate(nameof(example.Name)); // validationMessages.Count == 0

Example example2 = new Example();
example2.Name = null;

bool isObjectValid2 = example2.IsValid(); // isValid == false
bool isPropertyValid2 = example2.IsValid(nameof(example2.Name)); // isValid == false
ValidationResult objectValidationMessages2 = example2.Validate(); // validationMessages.Count == 1
var dict = objectValidationMessages2.GetAllFailures();
ValidationResult propertyValidationMessages2 = example2.Validate(nameof(example2.Name)); // validationMessages.Count == 1
