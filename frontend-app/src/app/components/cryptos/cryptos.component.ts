import {Component} from '@angular/core';
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
  cryptoList: { baseAsset: string, lastPrice: number }[] = [];
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
      next: (value: { baseAsset: string, lastPrice: number }) => {
        // Her bir veri için çalışacak işlemler
        const existingItem = this.cryptoList.find(item => item.baseAsset === value.baseAsset);
        if (existingItem) {
          // Varolan öğe bulundu, fiyatını güncelle
          existingItem.lastPrice = value.lastPrice;
          console.log("Güncellenen veri:", existingItem);
        } else {
          // Yeni öğe, ekle
          console.log("Yeni veri:", value);
          this.cryptoList.push(value);
        }
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
