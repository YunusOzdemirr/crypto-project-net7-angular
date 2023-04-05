namespace crypto_api.Models;

public class TradeOrderBid
{
    public decimal BestBidPrice { get; set; }
    public decimal BestBidQuantity { get; set; }
    public decimal Price{ get; set; }
}