using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace StockChart;

public class StockViewModel : INotifyPropertyChanged
   {
        private ObservableCollection<StockModel>? _stockPrices;
        private DateTime _rangeStart = new DateTime(2024, 1, 1);
        private DateTime _rangeEnd = new DateTime(2024, 6, 30);

        public DateTime RangeStart
        {
            get => _rangeStart;
            set
            {
                if (_rangeStart != value)
                {
                    _rangeStart = value;
                    OnPropertyChanged(nameof(RangeStart));
                }
            }
        }

        public DateTime RangeEnd
        {
            get => _rangeEnd;
            set
            {
                if (_rangeEnd != value)
                {
                    _rangeEnd = value;
                    OnPropertyChanged(nameof(RangeEnd));
                }
            }
        }

        public ObservableCollection<StockModel>? StockPrices
        {
            get => _stockPrices;
            set
            {
                if (_stockPrices != value)
                {
                    _stockPrices = value;
                    OnPropertyChanged(nameof(StockPrices));
                }
            }
        }

        public StockViewModel()
        {
            StockPrices = new ObservableCollection<StockModel>(ReadCSV("StockChart.Resources.Raw.amazon.csv").Reverse());
        }

        public ObservableCollection<StockModel> ReadCSV(string resourceStream)
        {
            var executingAssembly = typeof(App).GetTypeInfo().Assembly;
            using var inputStream = executingAssembly.GetManifestResourceStream(resourceStream)
                ?? throw new FileNotFoundException($"Resource {resourceStream} not found.");

            var lines = new List<string>();
            using (var reader = new StreamReader(inputStream))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            var formats = new[] { "MM/dd/yyyy", "M/d/yyyy", "M/dd/yyyy", "MM/d/yyyy" };
            return new ObservableCollection<StockModel>(
                lines.Select(line =>
                {
                    var data = line.Split(',');

                    if (data.Length < 6) return null;

                    if (DateTime.TryParseExact(data[0], formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
                    {
                        return new StockModel(
                            date,
                            Convert.ToDouble(data[1]),
                            Convert.ToDouble(data[2]),
                            Convert.ToDouble(data[3]),
                            Convert.ToDouble(data[4]),
                            Convert.ToDouble(data[5]) / 1000000
                        );
                    }

                    return null;
                })
                .Where(stock => stock != null)
                .ToList()!);
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
   }
