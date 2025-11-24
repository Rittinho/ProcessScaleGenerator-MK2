namespace ProcessScaleGenerator.View.Components.Elements;

public partial class TableInfoBlock : ContentView
{
    public static readonly BindableProperty TitleTextProperty =
    BindableProperty.Create(nameof(TitleText), typeof(string), typeof(EditorBar), default(string));

    public static readonly BindableProperty UnicodeProperty =
    BindableProperty.Create(nameof(Unicode), typeof(string), typeof(EditorBar), default(string));

    public static readonly BindableProperty CountProperty =
    BindableProperty.Create(nameof(Count), typeof(int), typeof(EditorBar), default(int));

    public static readonly BindableProperty DescriptionProperty =
    BindableProperty.Create(nameof(Description), typeof(string), typeof(EditorBar), default(string));

    public string TitleText
    {
        get => (string)GetValue(TitleTextProperty);
        set => SetValue(TitleTextProperty, value);
    }
    public string Unicode
    {
        get => (string)GetValue(UnicodeProperty);
        set => SetValue(UnicodeProperty, value);
    }
    public int Count
    {
        get => (int)GetValue(CountProperty);
        set => SetValue(CountProperty, value);
    }
    public string Description
    {
        get => (string)GetValue(DescriptionProperty);
        set => SetValue(DescriptionProperty, value);
    }

    public TableInfoBlock()
	{
		InitializeComponent();
	}
}