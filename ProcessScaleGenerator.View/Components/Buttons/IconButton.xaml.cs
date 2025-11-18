using System.Windows.Input;

namespace ProcessScaleGenerator.View.Components.Buttons;

public partial class IconButton : ContentView
{
    public static readonly BindableProperty UnicodeProperty =
    BindableProperty.Create(nameof(Unicode), typeof(string), typeof(IconButton), default(string));

    public static readonly BindableProperty TextContentProperty =
    BindableProperty.Create(nameof(TextContent), typeof(string), typeof(IconButton), default(string));

    public static readonly BindableProperty FontSizeProperty =
    BindableProperty.Create(nameof(FontSize), typeof(int), typeof(IconButton), default(int));

    public static readonly BindableProperty BackgroundColorCodeProperty =
    BindableProperty.Create(nameof(BackgroundColorCode), typeof(Color), typeof(IconButton), default(Color));

    public static readonly BindableProperty StrokeColorCodeProperty =
    BindableProperty.Create(nameof(StrokeColorCode), typeof(Color), typeof(IconButton), default(Color));

    public static readonly BindableProperty FontColorCodeProperty =
    BindableProperty.Create(nameof(FontColorCode), typeof(Color), typeof(IconButton), default(Color));

    public static readonly BindableProperty EventProperty =
    BindableProperty.Create(nameof(Event), typeof(ICommand), typeof(IconButton), default(ICommand));

    public static readonly BindableProperty EventParameterProperty =
    BindableProperty.Create(nameof(Event), typeof(object), typeof(IconButton), defaultValue: null);

    public string Unicode
    {
        get => (string)GetValue(UnicodeProperty);
        set => SetValue(UnicodeProperty, value);
    }
    public string TextContent
    {
        get => (string)GetValue(TextContentProperty);
        set => SetValue(TextContentProperty, value);
    }
    public int FontSize
    {
        get => (int)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }
    public Color BackgroundColorCode
    {
        get => (Color)GetValue(BackgroundColorCodeProperty);
        set => SetValue(BackgroundColorCodeProperty, value);
    }
    public Color StrokeColorCode
    {
        get => (Color)GetValue(StrokeColorCodeProperty);
        set => SetValue(StrokeColorCodeProperty, value);
    }
    public Color FontColorCode
    {
        get => (Color)GetValue(BackgroundColorCodeProperty);
        set => SetValue(BackgroundColorCodeProperty, value);
    }
    public ICommand Event
    {
        get => (ICommand)GetValue(EventProperty);
        set => SetValue(EventProperty, value);
    }
    public object EventParameter
    {
        get => GetValue(EventParameterProperty);
        set => SetValue(EventParameterProperty, value);
    }

    public IconButton()
    {
        InitializeComponent();
    }
}