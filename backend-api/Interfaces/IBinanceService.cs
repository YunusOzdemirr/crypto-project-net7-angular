using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;

namespace crypto_api.Interfaces;

public interface IBinanceService
{
    BinanceClient GetRestClient();
    BinanceSocketClient GetSocketClient();
    Task<IEnumerable<BinanceProduct>> GetProductsAsync();
}