import {Injectable} from '@angular/core';
import * as signalR from "@microsoft/signalr"

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

  public startConnection = () => {
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
  public dataStream = () => {
    this.hubConnection.stream("StreamStocks2").subscribe({
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
}


export interface ChartModel {
  data: [],
  label: string
  backgroundColor: string
}
