namespace ProcessScaleGenerator.View.Components.Elements;

public partial class DirectoryTextBar : ContentView
{
    public static readonly BindableProperty PathProperty =
BindableProperty.Create(nameof(Path), typeof(string), typeof(DirectoryTextBar), default(string));

    public static readonly BindableProperty TextTitleProperty =
    BindableProperty.Create(nameof(TextTitle), typeof(string), typeof(DirectoryTextBar), default(string));

    public static readonly BindableProperty UnicodeProperty =
    BindableProperty.Create(nameof(Unicode), typeof(string), typeof(DirectoryTextBar), default(string));

    public string TextTitle
    {
        get => (string)GetValue(TextTitleProperty);
        set => SetValue(TextTitleProperty, value);
    }
    public string Path
    {
        get => (string)GetValue(PathProperty);
        set => SetValue(PathProperty, value);
    }
    public string Unicode
    {
        get => (string)GetValue(UnicodeProperty);
        set => SetValue(UnicodeProperty, value);
    }
    public DirectoryTextBar()
	{
		InitializeComponent();
	}
}