namespace AstroTools.Events.Model;

public record AstroEvent(DateTime Date, string Name, string Description)
{
    public override string ToString()
    {
        return $"{nameof(Date)}: {Date}, {nameof(Name)}: {Name}, {nameof(Description)}: {Description}";
    }
}