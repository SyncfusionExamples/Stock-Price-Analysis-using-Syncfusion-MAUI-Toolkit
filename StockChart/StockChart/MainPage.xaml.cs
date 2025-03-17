namespace StockChart
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SfSegmentedControl_SelectionChanged(object sender, Syncfusion.Maui.Toolkit.SegmentedControl.SelectionChangedEventArgs e)
        {
            if (e.NewValue is Syncfusion.Maui.Toolkit.SegmentedControl.SfSegmentItem selectedItem)
            {
                DateTime startDate = new DateTime(2024, 01, 01);
                DateTime endDate = startDate;

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
    }
}