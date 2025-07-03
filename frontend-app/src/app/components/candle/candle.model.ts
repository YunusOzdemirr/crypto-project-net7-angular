
export interface CandleData {
    token: TradeDataContainer;
    chart: KlineDataContainer[];
}

export interface TradeDataContainer {
    id: number;
    oldPrice: number;
    symbol: string;
    name: string;
    asset: string;
    quote: string;
    lastPrice: number;
    priceChange: number;
    priceChangePercent: number;
    highPrice: number;
    lowPrice: number;
    volume: number;
    quoteVolume: number;
    openTime: Date;
    closeTime: Date;
}

export interface KlineDataContainer {
    open: number;
    high: number;
    low: number;
    close: number;
    timeStamp: Date;
}
