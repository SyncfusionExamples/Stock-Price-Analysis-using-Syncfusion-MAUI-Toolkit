namespace StockChart
{
    public class StockModel
    {
        public DateTime Date { get; }
        public double Open { get; }
        public double High { get; }
        public double Low { get; }
        public double Close { get; }
        public double Volume { get; }

        public StockModel(DateTime date, double open, double high, double low, double close, double volume)
        {
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }
    }
}
