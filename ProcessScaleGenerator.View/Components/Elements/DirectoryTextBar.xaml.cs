using CommunityToolkit.Maui.Storage;
using CommunityToolkit.Mvvm.Input;
using ProcessScaleGenerator.View.Components.Buttons;

namespace ProcessScaleGenerator.View.Components.Elements;

public partial class DirectoryTextBar : ContentView
{
    public static readonly BindableProperty PathFolderProperty =
BindableProperty.Create(nameof(PathFolder), typeof(string), typeof(DirectoryTextBar), default(string), BindingMode.TwoWay);

    public static readonly BindableProperty TextTitleProperty =
    BindableProperty.Create(nameof(TextTitle), typeof(string), typeof(DirectoryTextBar), default(string));

    public static readonly BindableProperty UnicodeProperty =
    BindableProperty.Create(nameof(Unicode), typeof(string), typeof(DirectoryTextBar), default(string));

    public static readonly BindableProperty UnicodeColorProperty =
    BindableProperty.Create(nameof(UnicodeColor), typeof(Color), typeof(IconButton), default(Color));

    public static readonly BindableProperty UnicodeFamilyProperty =
    BindableProperty.Create(nameof(UnicodeFamily), typeof(string), typeof(IconButton), default(string));

    public string UnicodeFamily
    {
        get => (string)GetValue(UnicodeFamilyProperty);
        set => SetValue(UnicodeFamilyProperty, value);
    }

    public Color UnicodeColor
    {
        get => (Color)GetValue(UnicodeColorProperty);
        set => SetValue(UnicodeColorProperty, value);
    }

    public string TextTitle
    {
        get => (string)GetValue(TextTitleProperty);
        set => SetValue(TextTitleProperty, value);
    }
    public string PathFolder
    {
        get => (string)GetValue(PathFolderProperty);
        set => SetValue(PathFolderProperty, value);
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

    [RelayCommand]
    private async Task DirectoryFolderPicker()
    {
        var result = await FolderPicker.Default.PickAsync();

        if (result.IsSuccessful)
        {
            PathFolder = result.Folder.Path;
        }
    }
}