import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ChartConfiguration, ChartType } from 'chart.js';
import { ApirequestComponent } from './components/apirequest/apirequest.component';
import {SignalrService} from "./services/signalr.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {

  chartOptions: ChartConfiguration['options'] = {
    responsive: true,
    scales: {
      y: {
        min: 0
      }
    }
  };

  chartLabels: string[] = ['Real time data for the chart'];
  chartType: ChartType = 'bar';
  chartLegend: boolean = true;

   constructor(public signalRService: SignalrService, private http: HttpClient) { }
  //constructor( private http: HttpClient) { }
  title = 'frontend-app';

  ngOnInit() {
   // this.apirequest.makeRequest();
   // this.signalRService.startConnection();
   // this.signalRService.dataStream();
  //  this.startHttpRequest();
  }

}
