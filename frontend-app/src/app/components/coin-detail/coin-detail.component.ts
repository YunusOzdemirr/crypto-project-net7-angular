import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { SignalrService } from 'src/app/services/signalr.service';
import { CandleData } from '../candle/candle.model';
import * as signalR from '@microsoft/signalr';

@Component({
  selector: 'app-coin-detail',
  templateUrl: './coin-detail.component.html',
  styleUrls: ['./coin-detail.component.scss'],
})
export class CoinDetailComponent implements OnInit {
  public candleData: CandleData | undefined;

  public hubConnection: signalR.HubConnection =
    new signalR.HubConnectionBuilder()
      .withUrl('https:localhost:5001/binance')
      .configureLogging(signalR.LogLevel.Debug)
      .build();
  constructor(
    private route: ActivatedRoute,
  ) {}

  async ngOnInit() {
    await this.StartMethod();
    this.dataStream();
  }

  public dataStream(): any {
    const symbol = this.route.snapshot.paramMap.get('symbol');
    console.log('symbol', symbol);
    console.log('SignalR connection state:', this.hubConnection.state);
    if (symbol && this.hubConnection.state === 'Connected') {
      console.log('Starting data stream for symbol:', symbol);
      this.hubConnection
        .stream('StreamStockHistory', symbol)
        .subscribe({
          next: (data: any) => {
            console.log('Received data:', data);
            this.candleData = data as CandleData;
          },
          complete: () => {},
          error: (err) => {
            console.error(err);
          },
        });
    }
  }

  async StartMethod() {
    await this.startConnection();
  }

  public async startConnection(): Promise<void> {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https:localhost:5001/binance')
      .configureLogging(signalR.LogLevel.Debug)
      .build();
    this.hubConnection
      .start()
      .then(() => {
        console.log('Connection started');
        this.dataStream();
      })
      .catch((err) => console.log('Error while starting connection: ' + err));
  }
}