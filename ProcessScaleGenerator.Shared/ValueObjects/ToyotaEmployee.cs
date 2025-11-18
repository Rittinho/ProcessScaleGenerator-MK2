namespace ProcessScaleGenerator.Shared.ValueObjects;

public class ToyotaEmployee(string CreationDate, string Name, string Position, bool hidded = false)
{
    public string CreationDate { get; set; } = CreationDate;
    public string Name { get; set; } = Name;
    public string Position { get; set; } = Position;
    public bool Hidded { get; set; } = hidded;

    public override bool Equals(object obj)
    => obj is ToyotaEmployee other && Name == other.Name;

    public override int GetHashCode()
        => Name.GetHashCode();
}
