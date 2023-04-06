import {Injectable} from '@angular/core';
import * as signalR from "@microsoft/signalr"

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  public data: ChartModel[] = [];
  private hubConnection: signalR.HubConnection = new signalR.HubConnectionBuilder()
    .withUrl("/binance")
    .build();
  public startConnection = () => {
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public dataStream = () => {
    this.hubConnection.stream('DataStream', (data: any) => {
      this.data = data;
      console.log(data);
    });
  }
}


export interface ChartModel {
  data: [],
  label: string
  backgroundColor: string
}
