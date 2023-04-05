using System.Collections.Concurrent;
using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Spot;
using crypto_api.Configurations;
using crypto_api.Interfaces;
using crypto_api.Models;
using CryptoExchange.Net.Sockets;
using Microsoft.AspNetCore.SignalR;

namespace crypto_api.Hubs;

public class BinanceHub : Hub
{
    private IBinanceService _binanceService;
    public static List<BinanceProduct> _symbols;
    public static ConcurrentDictionary<string, TradeDataContainer> TickerDictionary;
    public static ConcurrentDictionary<string, List<KlineDataContainer>> TickerDictionaryHistory;
    public static ConcurrentDictionary<string, decimal> OldDataDictionary;
    static int delay = 750;
    public static int sayac = 0;


    public BinanceHub(IBinanceService binanceService)
    {
        OldDataDictionary = new ConcurrentDictionary<string, decimal>();
        TickerDictionaryHistory = new ConcurrentDictionary<string, List<KlineDataContainer>>();
        TickerDictionary = new ConcurrentDictionary<string, TradeDataContainer>();
        _binanceService = binanceService;
        _symbols = _binanceService.GetProductsAsync().Result as List<BinanceProduct>;
        Thread backgroundThread = new Thread(new ThreadStart(Start));
        backgroundThread.Start();
    }

    private async void Start()
    {
        //  _ = await _socketClient.SpotStreams.SubscribeToAllTickerUpdatesAsync(ProcessTradeUpdate);
        //  _ = await _socketClient.UsdFuturesStreams.SubscribeToAllBookTickerUpdatesAsync(ProcessTradeUsd);
        _ = _binanceService.GetSocketClient().UsdFuturesStreams
            .SubscribeToAllTickerUpdatesAsync(ProcessTradeUsdUpdate).Result;
    }


    private static async void ProcessTradeUsdUpdate(DataEvent<IEnumerable<IBinance24HPrice>> dataEvent)
    {
        await Func();

        async Task Func()
        {
            foreach (var coin in dataEvent.Data)
            {
                var symbol = _symbols.Any(a => a.Symbol == coin.Symbol)
                    ? _symbols.Find(a => a.Symbol == coin.Symbol)
                    : null;

                if (symbol is not null)
                {
                    OldDataDictionary.TryGetValue(coin.Symbol, out var olddataprice);

                    if (TickerDictionary.TryGetValue(coin.Symbol, out TradeDataContainer oldData))
                    {
                        TickerDictionary[coin.Symbol] = new TradeDataContainer
                        {
                            Id = _symbols.FindIndex(a => a.Symbol == coin.Symbol),
                            Symbol = coin.Symbol,
                            PriceChange = coin.PriceChange,
                            PriceChangePercent = coin.PriceChangePercent,
                            Volume = coin.Volume,
                            OpenPrice = coin.OpenPrice,
                            HighPrice = coin.HighPrice,
                            LowPrice = coin.LowPrice,
                            ClosePrice = (decimal)symbol.ClosePrice,
                            LastPrice = coin.LastPrice,
                            LastPrice2 = olddataprice != 0 ? olddataprice : 0,
                            BaseAsset = symbol.BaseAsset,
                            BaseAssetName = symbol.BaseAssetName,
                            QuoteAsset = symbol.QuoteAsset,
                            QuoteAssetName = symbol.QuoteAssetName,
                            PrevDayClosePrice = coin.PriceChange,
                            QuoteVolume = coin.QuoteVolume,
                            WeightedAveragePrice = coin.WeightedAveragePrice,
                            TotalTrades = coin.TotalTrades,
                            CirculatingSupply = symbol.CirculatingSupply != null
                                ? (decimal)symbol.CirculatingSupply
                                : (decimal)0,
                            TargetLevel = oldData.TargetLevel <= coin.LastPrice
                                ? coin.LastPrice * (decimal)1.03
                                : oldData.TargetLevel,
                            StopLevel = oldData.StopLevel >= coin.LastPrice
                                ? coin.LastPrice * (decimal)0.97
                                : oldData.StopLevel,
                            TargetCount = oldData.TargetLevel <= coin.LastPrice ? +1 : oldData.TargetCount,
                            StopCount = oldData.StopLevel <= coin.LastPrice ? +1 : oldData.StopCount,
                            TradeOrderBids = oldData.TradeOrderBids,
                            TradeOrderAsks = oldData.TradeOrderAsks
                        };
                    }
                    else
                    {
                        TickerDictionary.TryAdd(coin.Symbol, new TradeDataContainer
                        {
                            Id = _symbols.FindIndex(a => a.Symbol == coin.Symbol),
                            Symbol = coin.Symbol,
                            PriceChange = coin.PriceChange,
                            PriceChangePercent = coin.PriceChangePercent,
                            Volume = coin.Volume,
                            OpenPrice = coin.OpenPrice,
                            HighPrice = coin.HighPrice,
                            LowPrice = coin.LowPrice,
                            ClosePrice = (decimal)symbol.ClosePrice,
                            LastPrice = coin.LastPrice,
                            LastPrice2 = olddataprice != 0 ? olddataprice : 0,
                            BaseAsset = symbol.BaseAsset,
                            BaseAssetName = symbol.BaseAssetName,
                            QuoteAsset = symbol.QuoteAsset,
                            QuoteAssetName = symbol.QuoteAssetName,
                            PrevDayClosePrice = coin.PriceChange,
                            QuoteVolume = coin.QuoteVolume,
                            WeightedAveragePrice = coin.WeightedAveragePrice,
                            TotalTrades = coin.TotalTrades,
                            CirculatingSupply = symbol.CirculatingSupply != null
                                ? (decimal)symbol.CirculatingSupply
                                : (decimal)0,
                            TargetLevel = coin.LastPrice * (decimal)1.03,
                            StopLevel = coin.LastPrice * (decimal)0.97,
                            TargetCount = 0,
                            StopCount = 0
                        });
                    }

                    if (sayac == 300)
                    {
                        OldDataDictionary.TryAdd(coin.Symbol, coin.LastPrice);
                    }

                    sayac++;

                    if (TickerDictionaryHistory.TryGetValue(coin.Symbol, out var existingItem))
                    {
                        var lastItem = existingItem.Last();
                        if (lastItem is null)
                        {
                            existingItem.RemoveAt(existingItem.Count - 1);
                            lastItem = existingItem.Last();
                        }

                        if (existingItem.Count() >= 30 && (DateTime.Now.Second - lastItem.TimeStamp.Second >= 6 ||
                                                           DateTime.Now.Minute - lastItem.TimeStamp.Minute >= 1))
                            existingItem.RemoveAt(0);
                        if (lastItem.LastPrice != coin.LastPrice &&
                            (DateTime.Now.Second - lastItem.TimeStamp.Second >= 6 ||
                             DateTime.Now.Minute - lastItem.TimeStamp.Minute >= 1))
                            existingItem.Add(new KlineDataContainer
                            {
                                LastPrice = coin.LastPrice,
                                TimeStamp = DateTime.Now
                            });
                    }
                    else
                        TickerDictionaryHistory.TryAdd(coin.Symbol, new List<KlineDataContainer>()
                        {
                            new KlineDataContainer
                            {
                                LastPrice = coin.LastPrice,
                                TimeStamp = DateTime.Now
                            }
                        });

                    //var ticker = TickerDictionaryHistory[coin.Symbol];
                    //if (ticker.IsEmpty) return;
                    //var copy = new List<DateTime>(ticker.Keys);
                    //copy.Where(x => (dataEvent.Timestamp - x).TotalSeconds >= 60).ToList().ForEach(x => ticker.TryRemove(x, out _));
                    await Task.Delay(750);
                }
            }
        }
    }
}