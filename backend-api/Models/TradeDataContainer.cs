using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Spot;

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

    public TradeDataContainer()
    {

    }
    public TradeDataContainer(int symbolIndex, decimal olddataprice, BinanceProduct symbol, IBinance24HPrice coin, TradeDataContainer oldData)
    {
        decimal targetLevel = 0, stopLevel = 0;
        int targetCount = 0, stopCount = 0;
        if (oldData is not null)
        {
            targetLevel = oldData.TargetLevel <= coin.LastPrice
                               ? coin.LastPrice * (decimal)1.03
                               : oldData.TargetLevel;
            stopLevel = oldData.StopLevel >= coin.LastPrice
                           ? coin.LastPrice * (decimal)0.97
                           : oldData.StopLevel;
            targetCount = oldData.TargetLevel <= coin.LastPrice ? +1 : oldData.TargetCount;
            stopCount = oldData.StopLevel <= coin.LastPrice ? +1 : oldData.StopCount;
        }
        else
        {
            targetLevel = coin.LastPrice * (decimal)1.03;
            stopLevel = coin.LastPrice * (decimal)0.97;
        }
        Id = symbolIndex;
        Symbol = coin.Symbol;
        PriceChange = coin.PriceChange;
        PriceChangePercent = coin.PriceChangePercent;
        Volume = coin.Volume;
        OpenPrice = coin.OpenPrice;
        HighPrice = coin.HighPrice;
        LowPrice = coin.LowPrice;
        ClosePrice = symbol.ClosePrice ?? 0m;
        LastPrice = coin.LastPrice;
        LastPrice2 = olddataprice != 0 ? olddataprice : 0;
        BaseAsset = symbol.BaseAsset;
        BaseAssetName = symbol.BaseAssetName;
        QuoteAsset = symbol.QuoteAsset;
        QuoteAssetName = symbol.QuoteAssetName;
        PrevDayClosePrice = coin.PriceChange;
        QuoteVolume = coin.QuoteVolume;
        WeightedAveragePrice = coin.WeightedAveragePrice;
        TotalTrades = coin.TotalTrades;
        CirculatingSupply = symbol.CirculatingSupply != null
                            ? (decimal)symbol.CirculatingSupply
                            : (decimal)0;
        TargetLevel = targetLevel;
        StopLevel = stopLevel;
        TargetCount = targetCount;
        StopCount = stopCount;
        TradeOrderBids = oldData?.TradeOrderBids;
        TradeOrderAsks = oldData?.TradeOrderAsks;
    }
}