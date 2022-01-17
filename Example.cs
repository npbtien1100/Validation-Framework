using ValidationFramework;
public class Example : Validatable
{
    [MustMatch(@"^[\w ]+$")]
    [CannotBeLongerThan(3)]
    [CannotBeNullOrEmpty()]
    public string? Name
    {
        get;
        set;
    }

    [MustBeBetween(1, 20)]
    public int age
    {
        get;
        set;
    }
}
