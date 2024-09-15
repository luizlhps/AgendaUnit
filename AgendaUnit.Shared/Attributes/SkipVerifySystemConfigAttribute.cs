namespace AgendaUnit.Shared.Attributes;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
public class SkipVerifySystemConfigAttribute : Attribute
{
}
