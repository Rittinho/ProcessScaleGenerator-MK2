
using System.Windows.Input;

namespace ProcessScaleGenerator.View.Components.Buttons;

public partial class SearchBar : ContentView
{
    public static readonly BindableProperty SearchTextProperty =
    BindableProperty.Create(nameof(SearchText), typeof(string), typeof(IconButton), default(string));

    public static readonly BindableProperty EventProperty =
    BindableProperty.Create(nameof(Event), typeof(ICommand), typeof(IconButton), default(ICommand));

    public static readonly BindableProperty EventParameterProperty =
    BindableProperty.Create(nameof(Event), typeof(object), typeof(IconButton), defaultValue: null);

    public string SearchText
    {
        get => (string)GetValue(SearchTextProperty);
        set => SetValue(SearchTextProperty, value);
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

    public SearchBar()
    {
        InitializeComponent();
    }
}