using Microsoft.Maui.Graphics.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProcessScaleGenerator.View.Components.Buttons.Base;
/// <summary>
/// Classe base que simula todas as propriedades funcionais de um Button nativo.
/// Herde desta classe para criar botões com layouts complexos (Grids, Stacks, Images).
/// </summary>
public class ButtonBaseView : ContentView
{
    #region Comandos e eventos
    public static readonly BindableProperty CommandProperty =
        BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(ButtonBaseView), null, propertyChanged: OnCommandChanged);

    public static readonly BindableProperty CommandParameterProperty =
        BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(ButtonBaseView), null);

    public event EventHandler Clicked;
    public event EventHandler Pressed;  // Opcional: Acionado ao tocar
    public event EventHandler Released; // Opcional: Acionado ao soltar

    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }
    #endregion

    #region Propiedades Visuais

    public static readonly BindableProperty CornerRadiusProperty =
        BindableProperty.Create(nameof(CornerRadius), typeof(CornerRadius), typeof(ButtonBaseView), default(CornerRadius));

    public static readonly BindableProperty BorderColorProperty =
        BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(ButtonBaseView), Colors.Transparent);

    public static readonly BindableProperty BorderWidthProperty =
        BindableProperty.Create(nameof(BorderWidth), typeof(double), typeof(ButtonBaseView), 0d);

    public new static readonly BindableProperty BackgroundColorProperty =
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(ButtonBaseView), Colors.Transparent);

    public new static readonly BindableProperty PaddingProperty =
    BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(ButtonBaseView), new Thickness(10, 5)); // Valor padrão confortável

    public new Thickness Padding
    {
        get => (Thickness)GetValue(PaddingProperty);
        set => SetValue(PaddingProperty, value);
    }

    public new Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public CornerRadius CornerRadius
    {
        get => (CornerRadius)GetValue(CornerRadiusProperty);
        set => SetValue(CornerRadiusProperty, value);
    }

    public Color BorderColor
    {
        get => (Color)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);
    }

    public double BorderWidth
    {
        get => (double)GetValue(BorderWidthProperty);
        set => SetValue(BorderWidthProperty, value);
    }
    #endregion

    #region Propiedades de Icone

    public static readonly BindableProperty UnicodeProperty =
        BindableProperty.Create(nameof(Unicode), typeof(string), typeof(ButtonBaseView), default(string));

    public static readonly BindableProperty UnicodeFamilyProperty =
        BindableProperty.Create(nameof(UnicodeFamily), typeof(string), typeof(ButtonBaseView), default(string));

    public static readonly BindableProperty HorizontalUnicodeAlignmentProperty =
        BindableProperty.Create(nameof(HorizontalUnicodetAlignment), typeof(TextAlignment), typeof(ButtonBaseView), TextAlignment.Center);

    public static readonly BindableProperty VerticalUnicodeAlignmentProperty =
        BindableProperty.Create(nameof(VerticalUnicodeAlignment), typeof(TextAlignment), typeof(ButtonBaseView), TextAlignment.Center);


    public string Unicode
    {
        get => (string)GetValue(UnicodeProperty);
        set => SetValue(UnicodeProperty, value);
    }

    public string UnicodeFamily
    {
        get => (string)GetValue(UnicodeFamilyProperty);
        set => SetValue(UnicodeFamilyProperty, value);
    }

    public TextAlignment HorizontalUnicodetAlignment
    {
        get => (TextAlignment)GetValue(HorizontalUnicodeAlignmentProperty);
        set => SetValue(HorizontalUnicodeAlignmentProperty, value);
    }

    public TextAlignment VerticalUnicodeAlignment
    {
        get => (TextAlignment)GetValue(VerticalUnicodeAlignmentProperty);
        set => SetValue(VerticalUnicodeAlignmentProperty, value);
    }

    #endregion

    #region Propiedades de texto

    [TypeConverter(typeof(FontSizeConverter))]
    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(ButtonBaseView), 14.0); 

    public static readonly BindableProperty TextColorProperty =
        BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(ButtonBaseView), Colors.Black);

    public static readonly BindableProperty FontFamilyProperty =
        BindableProperty.Create(nameof(FontFamily), typeof(string), typeof(ButtonBaseView), null);

    public static readonly BindableProperty FontAttributesProperty =
        BindableProperty.Create(nameof(FontAttributes), typeof(FontAttributes), typeof(ButtonBaseView), FontAttributes.None);

    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(ButtonBaseView), string.Empty);

    public static readonly BindableProperty HorizontalTextAlignmentProperty =
    BindableProperty.Create(nameof(HorizontalTextAlignment), typeof(TextAlignment), typeof(ButtonBaseView), TextAlignment.Center);

    public static readonly BindableProperty VerticalTextAlignmentProperty =
        BindableProperty.Create(nameof(VerticalTextAlignment), typeof(TextAlignment), typeof(ButtonBaseView), TextAlignment.Center);

    public static readonly BindableProperty LineBreakModeProperty =
        BindableProperty.Create(nameof(LineBreakMode), typeof(LineBreakMode), typeof(ButtonBaseView), LineBreakMode.NoWrap);

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public Color TextColor
    {
        get => (Color)GetValue(TextColorProperty);
        set => SetValue(TextColorProperty, value);
    }

    public string FontFamily
    {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }

    public FontAttributes FontAttributes
    {
        get => (FontAttributes)GetValue(FontAttributesProperty);
        set => SetValue(FontAttributesProperty, value);
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public TextAlignment HorizontalTextAlignment
    {
        get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
        set => SetValue(HorizontalTextAlignmentProperty, value);
    }

    public TextAlignment VerticalTextAlignment
    {
        get => (TextAlignment)GetValue(VerticalTextAlignmentProperty);
        set => SetValue(VerticalTextAlignmentProperty, value);
    }

    public LineBreakMode LineBreakMode
    {
        get => (LineBreakMode)GetValue(LineBreakModeProperty);
        set => SetValue(LineBreakModeProperty, value);
    }

    #endregion

    private readonly TapGestureRecognizer _tapGesture;

    public ButtonBaseView()
    {
        // Inicializa o reconhecedor de toque
        _tapGesture = new TapGestureRecognizer();
        _tapGesture.Tapped += OnTappedInternal;

        // Adiciona o gesto ao container
        this.GestureRecognizers.Add(_tapGesture);

        // Opacidade padrão ao desabilitar (semelhante ao nativo)
        this.PropertyChanged += OnPropertyChangedInternal;
    }

    private async void OnTappedInternal(object sender, EventArgs e)
    {
        if (!IsEnabled) return;

        // 1. Dispara evento Pressed (simulado)
        Pressed?.Invoke(this, EventArgs.Empty);

        // 2. Animação de Feedback Visual (O "Ripple" pobre)
        await this.FadeTo(0.7, 50);
        await this.FadeTo(1.0, 50);

        // 3. Executa o Command (MVVM)
        if (Command?.CanExecute(CommandParameter) == true)
        {
            Command.Execute(CommandParameter);
        }

        // 4. Dispara evento Clicked (Code Behind)
        Clicked?.Invoke(this, EventArgs.Empty);

        // 5. Dispara evento Released (simulado)
        Released?.Invoke(this, EventArgs.Empty);
    }

    // Monitora mudanças no Command para habilitar/desabilitar o botão automaticamente
    private static void OnCommandChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ButtonBaseView view)
        {
            if (oldValue is ICommand oldCommand)
                oldCommand.CanExecuteChanged -= view.OnCanExecuteChanged;

            if (newValue is ICommand newCommand)
                newCommand.CanExecuteChanged += view.OnCanExecuteChanged;
        }
    }

    private void OnCanExecuteChanged(object sender, EventArgs e)
    {
        // Atualiza o IsEnabled baseado no Command
        this.IsEnabled = Command?.CanExecute(CommandParameter) ?? true;
    }

    private void OnPropertyChangedInternal(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        // Efeito visual automático de "Desabilitado"
        if (e.PropertyName == nameof(IsEnabled))
        {
            this.Opacity = IsEnabled ? 1.0 : 0.5;
        }
    }
}