namespace ProcessScaleGenerator.View.Components.Elements;

public partial class EditorBar : ContentView
{
    public static readonly BindableProperty TextContentProperty =
   BindableProperty.Create(nameof(TextContent), typeof(string), typeof(EditorBar), default(string));

    public static readonly BindableProperty TextTitleProperty =
    BindableProperty.Create(nameof(TextTitle), typeof(string), typeof(EditorBar), default(string));

    public static readonly BindableProperty UnicodeProperty =
    BindableProperty.Create(nameof(Unicode), typeof(string), typeof(EditorBar), default(string));

    public static readonly BindableProperty HeightEditorProperty =
    BindableProperty.Create(nameof(HeightEditor), typeof(int), typeof(EditorBar), default(int));

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
    public int HeightEditor
    {
        get => (int)GetValue(HeightEditorProperty);
        set => SetValue(HeightEditorProperty, value);
    }
    public EditorBar()
	{
		InitializeComponent();
	}
}