import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {SignalrService} from "../../services/signalr.service";
import * as signalR from "@microsoft/signalr";
import {Observer} from "rxjs";
import {IStreamSubscriber} from "@microsoft/signalr";


@Component({
  selector: 'app-cryptos',
  templateUrl: './cryptos.component.html',
  styleUrls: ['./cryptos.component.scss']
})


export class CryptosComponent {

  displayedColumns: string[] = ['name', 'lastPrice', "priceChange", 'priceChangePercent', 'lowPrice', 'openPrice'];
  cryptoList: Stock[] = [];
  dataSource = this.cryptoList;

  public hubConnection: signalR.HubConnection = new signalR.HubConnectionBuilder()
    .withUrl("https:localhost:5001/binance")
    .configureLogging(signalR.LogLevel.Debug)
    .build();
  public subject: signalR.Subject<any> = new signalR.Subject();

  constructor(public signalRService: SignalrService) {
  }


  async ngOnInit() {
    await this.StartMethod();

    this.subject.subscribe(<IStreamSubscriber<any>>{
      next: (value: Stock) => {
        // Her bir veri için çalışacak işlemler
        const existingItem = this.cryptoList.find(item => item.baseAsset === value.baseAsset);
        if (existingItem) {
          existingItem.lastPrice = value.lastPrice;
          existingItem.priceChange = value.priceChange;
          existingItem.priceChangePercent = value.priceChangePercent;
          existingItem.openPrice = value.openPrice;
          existingItem.lowPrice = value.lowPrice;
          existingItem.highPrice = value.highPrice;
          //console.log("Güncellenen veri:", value);
          this.changeNode(value);
        } else {
          //console.log("Yeni veri:", value);
          this.cryptoList.push(value);
        }
        this.cryptoList = [...this.cryptoList];
      },
      complete: () => {
        // Tamamlandığında çalışacak işlemler
        console.log("Subject tamamlandı");
      },
      error: (error: any) => {
        // Hata durumunda çalışacak işlemler
        console.log("Subject error:", error);
      }
    });


  }

  public changeNode(value: Stock) {
    const stockNode = document.getElementById(value.baseAsset);
    if (stockNode) {
      var prevChange = parseFloat(stockNode!.textContent!);
      const priceChangeStockNode = document.getElementById(value.baseAsset + 'priceChange')!;
      const percentChangeStockNode = document.getElementById(value.baseAsset + 'Percent')!;
      const lowPriceStockNode = document.getElementById(value.baseAsset + 'lowPrice')!;
      const openPriceStockNode = document.getElementById(value.baseAsset + 'openPrice')!;
      const nameStockNode = document.getElementById(value.baseAsset + 'name')!;

      if (prevChange > value.lastPrice) {
        stockNode.className = "decrease";
        priceChangeStockNode.className = "decrease";
        percentChangeStockNode.className = "decrease";
        lowPriceStockNode.className = "decrease";
        openPriceStockNode.className = "decrease";
        nameStockNode.className = "decrease";
      } else if (prevChange < value.lastPrice) {
        stockNode.className = "increase";
        priceChangeStockNode.className = "increase";
        percentChangeStockNode.className = "increase";
        lowPriceStockNode.className = "increase";
        openPriceStockNode.className = "increase";
        nameStockNode.className = "increase";
      } else {
        return;
      }
    }
  }

  public async startConnection(): Promise<void> {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("https:localhost:5001/binance")
      .configureLogging(signalR.LogLevel.Debug)
      .build();
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
        this.dataStream();
      })
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  async StartMethod() {
    await this.startConnection();
  }

  public dataStream(): Promise<signalR.Subject<any>> {
    let isComplete = false;

    return new Promise<signalR.Subject<any>>((resolve, reject) => {
      this.hubConnection.stream("StreamStocks").subscribe(
        {
          closed: false,
          next: (value: any) => {
            resolve(value);
            this.subject.next(value);
          },
          complete: () => {
            if (isComplete) {
              console.log("dataStream tamamlandı");
            }
          },
          error: (err: any) => {
            console.log("dataStream error" + err);
            reject(err);
          }
        }
      );
    });
  }


}

interface Stock {
  id: number;
  baseAsset: string;
  baseAssetName: string;
  quoteAsset: string;
  quoteAssetName: string;
  symbol: string;
  lastPrice: number;
  lastPrice2: number;
  priceChange: number;
  volume: number;
  priceChangePercent: number;
  secondTime: number;
  highPrice: number;
  openPrice: number;
  lowPrice: number;
  closePrice: number;
  isClosed: boolean;
  prevDayClosePrice: number;
  quoteVolume: number;
  weightedAveragePrice: number;
  totalTrades: number;
  circulatingSupply: number;
  targetLevel: number;
  stopLevel: number;
  targetCount: number;
  stopCount: number;
  closeTime: string;
  openTime: string;
  date: string;
  tradeOrderAsks: any[];
  tradeOrderBids: any[];
}
