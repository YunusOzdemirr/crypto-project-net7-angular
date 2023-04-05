using Binance.Net.Clients;
using Binance.Net.Objects;
using Binance.Net.Objects.Models.Spot;
using crypto_api.Configurations;
using crypto_api.Interfaces;

namespace crypto_api.Services;

public class BinanceService : IBinanceService
{
    private BinanceConfiguration _binanceConfiguration;

    public BinanceService(IConfiguration configuration)
    {
        _binanceConfiguration = configuration.GetSection("BinanceConfiguration").Get<BinanceConfiguration>()!;
    }
    public async Task<IEnumerable<BinanceProduct>> GetProductsAsync()
    {
        return (await GetRestClient().SpotApi.ExchangeData.GetProductsAsync()).Data;
    }
    public  BinanceClient GetRestClient()
    {
        var binanceClient= new BinanceClient(new BinanceClientOptions()
        {
            ApiCredentials =
                new BinanceApiCredentials(_binanceConfiguration.Key, _binanceConfiguration.SecretKey),
        });
        return binanceClient;
    }

    public BinanceSocketClient GetSocketClient()
    {
        var binanceSocketClient = new BinanceSocketClient(new BinanceSocketClientOptions()
        {
            ApiCredentials =
                new BinanceApiCredentials(_binanceConfiguration.Key, _binanceConfiguration.SecretKey)
        });
        return binanceSocketClient;
    }
}