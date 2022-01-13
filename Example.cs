using ValidationFramework;
public class Example : Validatable
{
    [MustMatch(@"^[\w ]+$")]
    [CannotBeLongerThan(20)]
    [CannotBeNullOrEmpty()]
    public string? Name
    {
        get;
        set;
    }
}
