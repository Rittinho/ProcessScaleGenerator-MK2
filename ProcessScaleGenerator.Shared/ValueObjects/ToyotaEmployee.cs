namespace ProcessScaleGenerator.Shared.ValueObjects;

public class ToyotaEmployee(string CreationDate, string Name, string Position = "Descrição não realizada")
{
    public string CreationDate { get; set; } = CreationDate;
    public string Name { get; set; } = Name;
    public string Position { get; set; } = Position;

    public override bool Equals(object obj)
    => obj is ToyotaEmployee other && Name == other.Name;

    public override int GetHashCode()
        => Name.GetHashCode();
}
