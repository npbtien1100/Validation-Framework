using ValidationFramework;





Example example = new Example();
example.Name = "Test";
example.age = 15;

bool isObjectValid = example.IsValid(); // isValid == false
bool isPropertyValid = example.IsValid(nameof(example.Name)); // isValid == false
ValidationResult objectValidationMessages = example.Validate();
ValidationResult propertyValidationMessages = example.Validate(nameof(example.Name));

Example example2 = new Example();
example2.Name = null;
example2.age = 25;

bool isObjectValid2 = example2.IsValid(); // isValid == false
bool isPropertyValid2 = example2.IsValid(nameof(example2.Name)); // isValid == false
ValidationResult objectValidationMessages2 = example2.Validate();
var dict = objectValidationMessages2.GetAllFailures();
ValidationResult propertyValidationMessages2 = example2.Validate(nameof(example2.Name));
