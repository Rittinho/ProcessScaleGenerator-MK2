using ProcessScaleGenerator.Shared.Constants;

namespace ProcessScaleGenerator.View.Components.Elements;

public partial class CircularIcon : ContentView
{
    public static readonly BindableProperty IconSizeProperty =
    BindableProperty.Create(nameof(IconSize), typeof(int), typeof(CircularIcon), default(int), propertyChanged: OnIconSizePropertyChanged);

    public static readonly BindableProperty ColorCodeProperty =
    BindableProperty.Create(nameof(ColorCode), typeof(string), typeof(CircularIcon), default(string));

    public static readonly BindableProperty UnicodeProperty =
    BindableProperty.Create(nameof(Unicode), typeof(string), typeof(CircularIcon), default(string), propertyChanged: OnUnicodeChanged);

    public int IconSize
    {
        get => (int)GetValue(IconSizeProperty);
        set => SetValue(IconSizeProperty, value);
    }
    public string ColorCode
    {
        get => (string)GetValue(ColorCodeProperty);
        set => SetValue(ColorCodeProperty, value);
    }
    public string Unicode
    {
        get => (string)GetValue(UnicodeProperty);
        set => SetValue(UnicodeProperty, value);
    }
    public CircularIcon()
    {
        InitializeComponent();

        externalBorder.Padding = IconSize * 0.10;
        icon.FontSize = IconSize * 0.50;
    }
    public static void OnIconSizePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CircularIcon)bindable;
        control.externalBorder.Padding = (int)newValue * 0.07;
        control.icon.FontSize = (int)newValue * 0.50;
    }
    public static void OnUnicodeChanged(BindableObject bindable, object oldValue, object newValue)
    {
        var control = (CircularIcon)bindable;
        if (newValue is string unicode)
        {
            var code = FontAwesome.FASolid[unicode];
            control.icon.Text = code;
        }
    }
}