namespace ProcessScaleGenerator.Shared.ValueObjects;

public class ToyotaProcess(IconParameters icon, string creationDate, string title, string description)
{
    public string CreationDate { get; set; } = creationDate;
    public string Title { get; set; } = title;
    public string Description { get; set; } = description;
    public IconParameters Icon { get; set; } = icon;

    public override bool Equals(object obj)
        => obj is ToyotaProcess other && Title == other.Title;

    public override int GetHashCode()
        => Title.GetHashCode();
}
