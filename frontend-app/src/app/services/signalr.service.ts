import {Injectable} from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { Observable, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  public data: ChartModel[] = [];
  public hubConnection: signalR.HubConnection = new signalR.HubConnectionBuilder()
    .withUrl("https:localhost:5001/binance")
    .configureLogging(signalR.LogLevel.Debug)
    .build();
  public subject: signalR.Subject<any> = new signalR.Subject();

 public async startConnection():Promise<void>  {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl("https:localhost:5001/binance")
      .configureLogging(signalR.LogLevel.Debug)
      .build();
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
        this.dataStream2();
      })
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public dataStream = () => {
    this.hubConnection.stream("StreamStocks").subscribe({
      closed: false,
      next(value: any) {
        console.log("dataStream başladı");
        console.log(value);
      },
      complete() {
        console.log("dataStream tamamlandı");
      },
      error(err: any) {
        console.log("dataStream error" + err);
      },

    });
  }
   public async StreamStockHistory(symbol: string): Promise<Observable<any>> {

    const subject = new Subject<any>();
    this.hubConnection.stream('StreamStockHistory', symbol).subscribe({
      closed: false,
      next(value: any) {
        subject.next(value);
      },
      complete() {
        console.log('StreamStockHistory completed');
        subject.complete();
      },
      error(err: any) {
        console.log('StreamStockHistory error: ' + err);
        subject.error(err);
      },
    });

    return subject.asObservable();
  }

public dataStream2(): Observable<any> {
  const subject = new Subject<any>();

  this.hubConnection.stream("StreamStocks").subscribe({
    closed: false,
    next(value: any) {
      //console.log(value);
      subject.next(value); // Veriyi subject üzerinden yay
    },
    complete() {
      console.log("dataStream tamamlandı");
      subject.complete(); // İşlem tamamlandı
    },
    error(err: any) {
      console.log("dataStream error" + err);
      subject.error(err); // Hata durumunu subject üzerinden yay
    }
  });

  return subject.asObservable(); // Subject'i Observable olarak dön
}
}


export interface ChartModel {
  data: [],
  label: string
  backgroundColor: string
}
