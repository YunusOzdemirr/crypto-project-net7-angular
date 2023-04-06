using System.Runtime.CompilerServices;
using crypto_api.Models;

namespace crypto_api.Interfaces;

public interface IBinanceSocketService
{
    void Start();
    Task<IObservable<TradeDataContainer>> GetStocks();
    IAsyncEnumerable<object> GetDetailAsync(string symbol,
        [EnumeratorCancellation] CancellationToken cancellationToken);
    IAsyncEnumerable<object> DataStream([EnumeratorCancellation] CancellationToken cancellationToken);
}