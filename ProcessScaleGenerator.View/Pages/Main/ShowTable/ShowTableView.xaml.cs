using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls.Shapes;
using ProcessScaleGenerator.Shared.ValueObjects;
using ProcessScaleGenerator.View.Components.Elements;
using ProcessScaleGenerator.ViewModel.Pages.Main.ShowTable;

namespace ProcessScaleGenerator.View.Pages.Main.ShowTable;

public partial class ShowTableView : ContentPage
{
    public ShowTableView(ShowTableViewModel showTableViewModel)
    {
        InitializeComponent();
        BindingContext = showTableViewModel;

        TableGroupRender(showTableViewModel.Tables);
    }
    private void TableGroupRender(IEnumerable<ToyotaProcessTable> processGroups)
    {
        MainContainer.Clear();

        if (processGroups == null)
            return;
        var process = processGroups.ToArray();

        var tableGrid = new Grid { ColumnSpacing = 10, RowSpacing = 10};

        for (int j = 0; j < 5; j++)
        {
            tableGrid.ColumnDefinitions.Add(new ColumnDefinition(GridLength.Star));
        }

        for (int i = 0; i < 3; i++)
        {
            tableGrid.RowDefinitions.Add(new RowDefinition(GridLength.Star));

        }

        var index = 0;

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                if (process.Length > index)
                {
                    var view = ProcessTableContainer(process[index]);
                    index++;
                    tableGrid.Add(view, j, i);
                }
                continue;
            }
        }

        MainContainer.Children.Add(tableGrid);
    }
    private Border ProcessTableContainer(ToyotaProcessTable processGroup)
    {
        var employeeLayout = new VerticalStackLayout { Spacing = 10, VerticalOptions = LayoutOptions.Center };

        foreach (var emp in processGroup.Employees)
            employeeLayout.Children.Add(EmployeContainer(emp));

        var iconFild = new VerticalStackLayout { Spacing = 5 };

        var icon = new CircularIcon
        {
            IconSize = 130,
            Unicode = processGroup.Process.Icon.Unicode,
            ColorCode = processGroup.Process.Icon.ColorCode
        };

        var titleLabel = new Label
        {
            Text = processGroup.Process.Title,
            TextColor = Colors.White,
            HorizontalOptions = LayoutOptions.Center,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalOptions = LayoutOptions.Center,
            FontAttributes = FontAttributes.Bold,
            FontSize = 30
        };

        iconFild.Children.Add(icon);
        iconFild.Children.Add(titleLabel);

        var iconBorder = new Border
        {
            BackgroundColor = Color.FromRgba("323232"),
            StrokeThickness = 0,
            Padding = 10,
            StrokeShape = new RoundRectangle { CornerRadius = 14 },
            Content = iconFild
        };

        var mainGrid = new Grid
        {
            RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition(GridLength.Auto),
                new RowDefinition(GridLength.Star)
            },
            RowSpacing = 5
        };

        mainGrid.Add(iconBorder, 0, 0);
        mainGrid.Add(employeeLayout, 0, 1);

        var outerBorder = new Border
        {
            BackgroundColor = Color.FromRgba("323232"),
            StrokeThickness = 0,
            Padding = 10,
            StrokeShape = new RoundRectangle { CornerRadius = 14 },
            Content = mainGrid
        };

        return outerBorder;
    }
    private Border EmployeContainer(ToyotaEmployee employee)
    {
        var nameLabel = new Label
        {
            Text = employee.Name,
            HorizontalTextAlignment = TextAlignment.Center,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center
        };

        var border = new Border
        {
            BackgroundColor = Color.FromArgb("424242"),
            StrokeThickness = 0,
            StrokeShape = new RoundRectangle { CornerRadius = 24 },
            Padding = new Thickness(10, 5, 10, 5),
            Content = nameLabel,
            VerticalOptions = LayoutOptions.Center 
        };

        return border;
    }
}

