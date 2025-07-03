using Binance.Net.Interfaces;
using Binance.Net.Objects.Models.Spot;
using crypto_api.Interfaces;
using crypto_api.Models;
using CryptoExchange.Net.Sockets;
using System.Collections.Concurrent;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;

namespace crypto_api.Services;

public class BinanceSocketService : IBinanceSocketService
{
    public static ConcurrentDictionary<string, TradeDataContainer> TickerDictionary;
    public static ConcurrentDictionary<string, List<KlineDataContainer>> TickerDictionaryHistory;
    public static ConcurrentDictionary<string, decimal> OldDataDictionary;
    private IBinanceService _binanceService;
    public static List<BinanceProduct> _symbols;
    public const int delay = 750;
    public int counter = 0;
    public readonly Subject<TradeDataContainer> _subject = new();

    public BinanceSocketService(IBinanceService binanceService)
    {
        OldDataDictionary = new ConcurrentDictionary<string, decimal>();
        TickerDictionaryHistory = new ConcurrentDictionary<string, List<KlineDataContainer>>();
        TickerDictionary = new ConcurrentDictionary<string, TradeDataContainer>();
        _binanceService = binanceService;
        _symbols = _binanceService.GetProductsAsync().Result as List<BinanceProduct>;
        if (_symbols is null)
        {
            _symbols = new List<BinanceProduct>()
            {
                new BinanceProduct()
                {
                    Symbol = "BTCUSDT",
                    BaseAsset = "BTC",
                    QuoteAsset = "USDT",
                    BaseAssetName = "Bitcoin",
                    QuoteAssetName = "Tether"
                }
            };
        }
        Thread backgroundThread = new Thread(new ThreadStart(Start));
        backgroundThread.Start();
    }

    public async void Start()
    {
        //  _ = await _socketClient.SpotStreams.SubscribeToAllTickerUpdatesAsync(ProcessTradeUpdate);
        //  _ = await _socketClient.UsdFuturesStreams.SubscribeToAllBookTickerUpdatesAsync(ProcessTradeUsd);
        _ = _binanceService.GetSocketClient().UsdFuturesStreams
            .SubscribeToAllTickerUpdatesAsync(ProcessTradeUsdUpdate).Result;
    }

    public async Task<IObservable<TradeDataContainer>> GetStocks(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return await Task.FromResult(_subject);
    }

    public async IAsyncEnumerable<object> GetDetailAsync(string symbol,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (TickerDictionary.TryGetValue(symbol, out TradeDataContainer data))
            {
                var klines = await _binanceService.GetRestClient().SpotApi.ExchangeData.GetKlinesAsync(symbol, Binance.Net.Enums.KlineInterval.OneMinute, limit: 100);
                var chart = klines.Data.Select(k => new KlineDataContainer
                {
                    Open = k.OpenPrice,
                    High = k.HighPrice,
                    Low = k.LowPrice,
                    Close = k.ClosePrice,
                    TimeStamp = k.OpenTime
                }).ToList();

                yield return new { Token = data, Chart = chart };
            }

            await Task.Delay(delay, cancellationToken);
        }
    }

    

    private async void ProcessTradeUsdUpdate(DataEvent<IEnumerable<IBinance24HPrice>> dataEvent)
    {
        foreach (var coin in dataEvent.Data)
        {
            var symbol = _symbols.Any(a => a.Symbol == coin.Symbol)
                ? _symbols.Find(a => a.Symbol == coin.Symbol)
                : null;

            if (symbol is null)
                continue;
            OldDataDictionary.TryGetValue(coin.Symbol, out var olddataprice);

            if (TickerDictionary.TryGetValue(coin.Symbol, out TradeDataContainer oldData))
            {
                TickerDictionary[coin.Symbol] = new TradeDataContainer(oldData.Id, olddataprice, symbol, coin, oldData);
                _subject.OnNext(TickerDictionary[coin.Symbol]);
            }
            else
            {
                int symbolIndex = _symbols.FindIndex(a => a.Symbol == coin.Symbol);
                TickerDictionary.TryAdd(coin.Symbol, new TradeDataContainer(symbolIndex, olddataprice, symbol, coin, null));
                _subject.OnNext(TickerDictionary[coin.Symbol]);
            }

            if (counter == 300)
                OldDataDictionary.TryAdd(coin.Symbol, coin.LastPrice);

            counter++;

            await Task.Delay(750);
        }
    }


}