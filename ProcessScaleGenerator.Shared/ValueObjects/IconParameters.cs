namespace ProcessScaleGenerator.Shared.ValueObjects;

public class IconParameters(string Unicode = "Asterisk", string ColorCode = "00FF00")
{
    public string Unicode { get; set; } = Unicode;
    public string ColorCode { get; set; } = ColorCode;
}
