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
}
