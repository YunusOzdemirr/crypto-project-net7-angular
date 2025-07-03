using crypto_api.Models;
using System.Runtime.CompilerServices;

namespace crypto_api.Interfaces;

public interface IBinanceSocketService
{
    void Start();
    Task<IObservable<TradeDataContainer>> GetStocks(CancellationToken cancellationToken = default);
    IAsyncEnumerable<object> GetDetailAsync(string symbol,
        [EnumeratorCancellation] CancellationToken cancellationToken);
    
}