namespace AgendaUnit.Shared.Attributes;

[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class DateRangeAttribute : Attribute
{
    public string ReferencedProperty { get; }

    public DateRangeAttribute(string referencedProperty)
    {
        ReferencedProperty = referencedProperty;

    }

}
