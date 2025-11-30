using ProcessScaleGenerator.View.Components.Buttons.Base;

namespace ProcessScaleGenerator.View.Components.Buttons;

public partial class FreshSwitch : ButtonBaseView
{
    public static readonly BindableProperty LeftTextProperty =
    BindableProperty.Create(nameof(LeftText), typeof(string), typeof(FreshSwitch), default(string));

    public static readonly BindableProperty RightTextProperty =
    BindableProperty.Create(nameof(RightText), typeof(string), typeof(FreshSwitch), default(string));

    public static readonly BindableProperty IsToggledProperty =
        BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(FreshSwitch), defaultValue:false, BindingMode.TwoWay, propertyChanged: OnValueChanged);

    public static readonly BindableProperty OnColorProperty =
        BindableProperty.Create(nameof(OnColor), typeof(Color), typeof(FreshSwitch), Colors.Transparent);

    public static readonly BindableProperty OnThumbColorProperty =
        BindableProperty.Create(nameof(OnThumbColor), typeof(Color), typeof(FreshSwitch), Colors.White);

    public static readonly BindableProperty OffThumbColorProperty =
        BindableProperty.Create(nameof(OffThumbColor), typeof(Color), typeof(FreshSwitch), Colors.Gray);

    public new static readonly BindableProperty SpacingProperty =
        BindableProperty.Create(nameof(Spacing), typeof(int), typeof(FreshSwitch), defaultValue:0);
    public new int Spacing
    {
        get => (int)GetValue(SpacingProperty);
        set => SetValue(SpacingProperty, value);
    }

    public string LeftText
    {
        get => (string)GetValue(LeftTextProperty);
        set => SetValue(LeftTextProperty, value);
    }
    public string RightText
    {
        get => (string)GetValue(RightTextProperty);
        set => SetValue(RightTextProperty, value);
    }

    public bool IsToggled
    {
        get => (bool)GetValue(IsToggledProperty);
        set => SetValue(IsToggledProperty, value);
    }
    public Color OnColor
    {
        get => (Color)GetValue(OnColorProperty);
        set => SetValue(OnColorProperty, value);
    }
    public Color OnThumbColor
    {
        get => (Color)GetValue(OnThumbColorProperty);
        set => SetValue(OnThumbColorProperty, value);
    }
    public Color OffThumbColor
    {
        get => (Color)GetValue(OffThumbColorProperty);
        set => SetValue(OffThumbColorProperty, value);
    }

    public FreshSwitch()
	{
		InitializeComponent();
        AnimateStateAsync();
    }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is FreshSwitch btn)
        {
            _ = btn.AnimateStateAsync();
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        IsToggled = !IsToggled;
    }
    private async Task AnimateStateAsync()
    {
        // Cores
        SwitchRoot.BackgroundColor = IsToggled ? OnColor : Colors.Transparent;
        Thumb.BackgroundColor = IsToggled ? OnThumbColor : OffThumbColor;
        // Animação de Posição
        // Espaço disponível = Largura Total - Largura do Thumb - Padding Total (2 na esq + 2 na dir = 4)
        double availableWidth = SwitchRoot.Width - Thumb.Width - 6;

        if (availableWidth <= 0) availableWidth = 20; // Fallback se o layout não tiver sido calculado

        if (IsToggled)
        {
            await Thumb.TranslateTo(availableWidth, 0, 250, Easing.CubicOut);
        }
        else
        {
            await Thumb.TranslateTo(0, 0, 250, Easing.CubicOut);
        }
    }
}