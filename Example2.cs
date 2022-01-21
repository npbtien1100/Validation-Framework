using ValidationFramework;
public class Example2 : Validatable
{
    [MustBeGreaterThan(3)]
    public int? Relatives
    {
        get;
        set;
    }

    [CannotBeLongerThan(4)]
    public string? LastName
    {
        get;
        set;
    }

}
