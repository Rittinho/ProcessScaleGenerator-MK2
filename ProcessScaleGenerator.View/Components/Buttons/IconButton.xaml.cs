using ProcessScaleGenerator.View.Components.Buttons.Base;
using System.Windows.Input;

namespace ProcessScaleGenerator.View.Components.Buttons;

public partial class IconButton : ButtonBaseView
{
    public enum IconAlignment { Left, Right, Top, Bottom }

    public static readonly BindableProperty IconPositionProperty =
        BindableProperty.Create(nameof(IconPosition), typeof(IconAlignment), typeof(IconButton), defaultValue: IconAlignment.Left, propertyChanged: OnIconPositionChanged);

    public IconAlignment IconPosition
    {
        get => (IconAlignment)GetValue(IconPositionProperty);
        set => SetValue(IconPositionProperty, value);
    }

    public IconButton()
    {
        InitializeComponent();

        UpdateGrid();
    }

    private static void OnIconPositionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        // Converte o objeto genérico de volta para o nosso botão e roda a lógica
        if (bindable is IconButton btn)
        {
            btn.UpdateGrid();
        }
    }

    private void UpdateGrid()
    {
        switch (IconPosition)
        {
            case IconAlignment.Left:
                IconLeftButton();
                break;
            case IconAlignment.Right:
                IconRightButton();
                break;
            case IconAlignment.Top:
                IconTopButton();
                break;
            case IconAlignment.Bottom:
                IconBottomButton();
                break;
        }
    }

    private void IconLeftButton()
    {
        ButtonGrid.RowDefinitions.Clear();
        ButtonGrid.ColumnDefinitions.Clear();
        ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
        ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        ButtonGrid.RowSpacing = 0;
        ButtonGrid.ColumnSpacing = 5;

        Grid.SetColumn(Icon, 0);
        Grid.SetColumn(Text, 1);
    }
    private void IconRightButton()
    {
        ButtonGrid.RowDefinitions.Clear();
        ButtonGrid.ColumnDefinitions.Clear();
        ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        ButtonGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
        ButtonGrid.RowSpacing = 0;
        ButtonGrid.ColumnSpacing = 5;

        Grid.SetColumn(Text, 0);
        Grid.SetColumn(Icon, 1);
    }
    private void IconTopButton()
    {
        ButtonGrid.ColumnDefinitions.Clear();
        ButtonGrid.RowDefinitions.Clear();
        ButtonGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        ButtonGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
        ButtonGrid.RowSpacing = 5;
        ButtonGrid.ColumnSpacing = 0;

        Grid.SetRow(Icon, 0);
        Grid.SetRow(Text, 1);
    }
    private void IconBottomButton()
    {
        ButtonGrid.ColumnDefinitions.Clear();
        ButtonGrid.RowDefinitions.Clear();
        ButtonGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Star });
        ButtonGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
        ButtonGrid.RowSpacing = 5;
        ButtonGrid.ColumnSpacing = 0;

        Grid.SetRow(Text, 0);
        Grid.SetRow(Icon, 1);
    }
}