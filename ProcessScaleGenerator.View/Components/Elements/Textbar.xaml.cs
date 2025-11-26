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

    public static readonly BindableProperty UnicodeFamilyProperty =
    BindableProperty.Create(nameof(UnicodeFamily), typeof(string), typeof(IconButton), default(string));

    public static readonly BindableProperty UnicodeColorProperty =
    BindableProperty.Create(nameof(UnicodeColor), typeof(Color), typeof(IconButton), default(Color));

    public string UnicodeFamily
    {
        get => (string)GetValue(UnicodeFamilyProperty);
        set => SetValue(UnicodeFamilyProperty, value);
    }
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
    public Color UnicodeColor
    {
        get => (Color)GetValue(UnicodeColorProperty);
        set => SetValue(UnicodeColorProperty, value);
    }
    public Textbar()
	{
		InitializeComponent();
	}
}