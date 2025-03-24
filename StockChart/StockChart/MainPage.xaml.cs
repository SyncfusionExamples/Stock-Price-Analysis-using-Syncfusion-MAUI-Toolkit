using Syncfusion.Maui.Toolkit.Charts;

namespace StockChart;

public partial class MainPage : ContentPage
{
    int _month = int.MaxValue;
    public MainPage()
    {
        InitializeComponent();
        if(DeviceInfo.Platform == DevicePlatform.WinUI || DeviceInfo.Platform == DevicePlatform.MacCatalyst)
        {
            MyBorder.Style = (Style)Resources["PlatformSpecificBorder"];
        }
    }
    private void SfSegmentedControl_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.SegmentedControl.SelectionChangedEventArgs e)
    {
        if (e.NewValue is Syncfusion.Maui.Toolkit.SegmentedControl.SfSegmentItem selectedItem)
        {
            DateTime startDate = new DateTime(2024, 01, 01);
            DateTime endDate = new DateTime(2024, 06, 30);

            switch (selectedItem.Text)
            {
                case "3m":
                    endDate = startDate.AddMonths(3);
                    break;
                case "6m":
                    endDate = startDate.AddMonths(6);
                    break;
                case "9m":
                    endDate = startDate.AddMonths(9);
                    break;
                case "1y":
                    endDate = startDate.AddYears(1);
                    break;
                case "All":
                    startDate = new DateTime(2023, 11, 08);
                    endDate = new DateTime(2025, 02, 07);
                    break;
            }

            viewModel.RangeStart = startDate;
            viewModel.RangeEnd = endDate;
        }
        segmentedControl.SelectionIndicatorSettings = new Syncfusion.Maui.Toolkit.SegmentedControl.SelectionIndicatorSettings
        {
            Background = Colors.White,
            TextColor = Colors.Black
        };
    }

    private void Primary_LabelCreated(object sender, Syncfusion.Maui.Toolkit.Charts.ChartAxisLabelEventArgs e)
    {

        DateTime baseDate = new(2023, 11, 08);
        var date = baseDate.AddDays(e.Position);
        if (date.Month != _month)
        {
            ChartAxisLabelStyle labelStyle = new()
            {
                LabelFormat = "MMM-dd",
                FontAttributes = FontAttributes.Bold
            };
            e.LabelStyle = labelStyle;
            _month = date.Month;
        }
        else
        {
            ChartAxisLabelStyle labelStyle = new()
            {
                LabelFormat = "dd"
            };
            e.LabelStyle = labelStyle;
        }
    }
}

