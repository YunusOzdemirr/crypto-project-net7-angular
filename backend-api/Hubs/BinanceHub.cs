using crypto_api.Extensions;
using crypto_api.Interfaces;
using crypto_api.Models;
using Microsoft.AspNetCore.SignalR;
using System.Runtime.CompilerServices;
using System.Threading.Channels;


namespace crypto_api.Hubs;

public class BinanceHub : Hub
{
    private IBinanceSocketService _binanceSocketService;

    public BinanceHub(IBinanceSocketService binanceSocketService)
    {
        _binanceSocketService = binanceSocketService;
    }

    public async Task<ChannelReader<TradeDataContainer>> StreamStocks(CancellationToken cancellationToken = default)
    {
        return (await _binanceSocketService.GetStocks(cancellationToken)).AsChannelReader(10);
    }
    public async IAsyncEnumerable<object> StreamStockHistory(string symbol, [EnumeratorCancellation]CancellationToken cancellationToken = default)
    {
        var result = _binanceSocketService.GetDetailAsync(symbol, cancellationToken);
        await foreach (var item in result)
        {
            cancellationToken.ThrowIfCancellationRequested();
            yield return item;
        }
    }
}