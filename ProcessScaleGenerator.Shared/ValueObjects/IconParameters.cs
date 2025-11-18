namespace ProcessScaleGenerator.Shared.ValueObjects;

public class IconParameters(string Unicode = "Asterisk", string ColorCode = "FFFFFF")
{
    public string Unicode { get; set; } = Unicode;
    public string ColorCode { get; set; } = ColorCode;
}
