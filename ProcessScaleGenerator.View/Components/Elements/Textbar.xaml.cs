using ProcessScaleGenerator.View.Components.Buttons;
using System.Windows.Input;

namespace ProcessScaleGenerator.View.Components.Elements;

public partial class Textbar : ContentView
{
    public static readonly BindableProperty TextContentProperty =
    BindableProperty.Create(nameof(TextContent), typeof(string), typeof(Textbar), default(string));

    public static readonly BindableProperty TextTitleProperty =
    BindableProperty.Create(nameof(TextTitle), typeof(string), typeof(Textbar), default(string));

    public static readonly BindableProperty UnicodeProperty =
    BindableProperty.Create(nameof(Unicode), typeof(string), typeof(Textbar), default(string));

    public string TextTitle
    {
        get => (string)GetValue(TextTitleProperty);
        set => SetValue(TextTitleProperty, value);
    }
    public string TextContent
    {
        get => (string)GetValue(TextContentProperty);
        set => SetValue(TextContentProperty, value);
    }
    public string Unicode
    {
        get => (string)GetValue(UnicodeProperty);
        set => SetValue(UnicodeProperty, value);
    }
    public Textbar()
	{
		InitializeComponent();
	}
}