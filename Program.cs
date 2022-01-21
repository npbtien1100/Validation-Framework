using ValidationFramework;



Example2 example2 = new Example2();
example2.Relatives = 2;
example2.LastName = "Nguyen";

Example example = new Example();
example.Address = "adress 123";
example.Phone = "123456789";
example.Example2 = example2;

User user = new User();
user.Name = "Bao Tien";
user.age = 25;
user.Example = example;

ValidationResult objectValidationMessages2 = user.Validate();
var dict = objectValidationMessages2.GetAllFailures();
ValidationResult propertyValidationMessages2 = user.Validate(nameof(user.Example));
var temp = propertyValidationMessages2.GetAllFailures();