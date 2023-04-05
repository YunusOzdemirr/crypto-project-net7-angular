namespace crypto_api.Models;

public class KlineDataContainer
{
    //public int Id { get; set; }
    //public string Symbol { get; set; }
    //public decimal PriceChange { get; set; }
    //public decimal PriceChangePercent { get; set; }
    // public List<DateTime> TimeStamps { get; set; } = new List<DateTime>();
    // public List<decimal> Prices { get; set; } = new List<decimal>();
    public decimal LastPrice { get; set; }
    public DateTime TimeStamp { get; set; }
}