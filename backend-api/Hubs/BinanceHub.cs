using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Reactive.Subjects;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using Binance.Net.Clients;
using Binance.Net.Interfaces;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Spot;
using crypto_api.Configurations;
using crypto_api.Interfaces;
using crypto_api.Models;
using CryptoExchange.Net.CommonObjects;
using CryptoExchange.Net.Sockets;
using Microsoft.AspNetCore.SignalR;
using crypto_api.Extensions;
using crypto_api.Services;


namespace crypto_api.Hubs;

public class BinanceHub : Hub
{
    private IBinanceSocketService _binanceSocketService;
    public BinanceHub(IBinanceSocketService binanceSocketService)
    {
        _binanceSocketService = binanceSocketService;
    }

    public async Task<ChannelReader<TradeDataContainer>> StreamStocks()
    {
        return (await _binanceSocketService.GetStocks()).AsChannelReader(10);
    }
}