namespace crypto_api.Models;

public class TradeOrderAsk
{
    public decimal BestAskPrice { get; set; }
    public decimal BestAskQuantity { get; set; }
    public decimal Price { get; set; }
}