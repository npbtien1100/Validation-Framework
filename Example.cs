using ValidationFramework;
public class Example : Validatable
{
    [MustMatch(@"^[\w ]+$")]
    [CannotBeLongerThan(10)]
    [CannotBeNullOrEmpty()]
    public string? Address
    {
        get;
        set;
    }

    [CannotBeLongerThan(8)]
    public string? Phone
    {
        get;
        set;
    }

    [NestedValidation()]
    [CannotBeNull()]
    public Example2? Example2
    {
        get;
        set;

    }
}
