namespace AgendaUnit.Shared.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class SortableAttribute : Attribute
{
    public bool IsDescending { get; set; }

    public SortableAttribute(bool isDescending = false)
    {
        IsDescending = isDescending;
    }
}
