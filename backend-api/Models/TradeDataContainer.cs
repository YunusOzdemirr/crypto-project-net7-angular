namespace crypto_api.Models;

public class TradeDataContainer
{
    public int Id { get; set; }
    public string BaseAsset { get; set; }
    public string BaseAssetName { get; set; }
    public string QuoteAsset { get; set; }
    public string QuoteAssetName { get; set; }
    public string Symbol { get; set; }
    public decimal LastPrice { get; set; }
    public decimal LastPrice2 { get; set; }
    public decimal PriceChange { get; set; }
    public decimal Volume { get; set; }
    public decimal PriceChangePercent { get; set; }
    public int SecondTime { get; init; }
    public decimal HighPrice { get; set; }
    public decimal OpenPrice { get; set; }
    public decimal LowPrice { get; set; }
    public decimal ClosePrice { get; set; }
    public bool IsClosed { get; set; }
    public decimal PrevDayClosePrice { get; set; }
    public decimal QuoteVolume { get; set; }
    public decimal WeightedAveragePrice { get; set; }
    public long TotalTrades { get; set; }
    public decimal CirculatingSupply { get; set; }
    public decimal TargetLevel { get; set; }
    public decimal StopLevel { get; set; }
    public int TargetCount { get; set; }
    public int StopCount { get; set; }
    public DateTime CloseTime { get; set; }
    public DateTime OpenTime { get; set; }
    public DateTime Date { get; set; }
    public List<TradeOrderAsk> TradeOrderAsks { get; set; } = new List<TradeOrderAsk>();
    public List<TradeOrderBid> TradeOrderBids { get; set; } = new List<TradeOrderBid>();

}