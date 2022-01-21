using ValidationFramework;
public class User : Validatable
{
    [MustMatch(@"^[\w ]+$")]
    [CannotBeLongerThan(5)]
    [CannotBeNullOrEmpty()]
    public string? Name
    {
        get;
        set;
    }

    [NestedValidation()]
    [MustBeBetween(1, 20)]
    public int age
    {
        get;
        set;
    }
    [NestedValidation()]
    [CannotBeNull()]
    public Example? Example
    {
        get;
        set;
    }
}
