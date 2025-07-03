import { Component, Input, OnInit } from '@angular/core';
import { Chart, ChartConfiguration, ChartType, ChartData } from 'chart.js';
import {
  CandlestickController,
  CandlestickElement,
} from 'chartjs-chart-financial';
import { KlineDataContainer } from './candle.model';

@Component({
  selector: 'app-candle',
  templateUrl: './candle.component.html',
  styleUrls: ['./candle.component.scss'],
})
export class CandleComponent implements OnInit {
  @Input() chartData: KlineDataContainer[] = [];

  public candleChartData: ChartConfiguration['data'] = {
    datasets: [],
  };
  public candleChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    maintainAspectRatio: false,
  };
  public candleChartType: ChartType = 'candlestick';
  constructor() {
    Chart.register(CandlestickController, CandlestickElement);
  }

  ngOnInit(): void {
    Chart.register(CandlestickController, CandlestickElement);
    this.updateChart();
  }

  ngOnChanges(): void {
    this.updateChart();
  }

  updateChart() {
    if (!this.chartData || this.chartData.length === 0) {
      console.log('No chart data available');
      return;
    }
    const chartData: ChartData<'candlestick'> = {
      datasets: [
        {
          label: 'Price',
          data: this.chartData.map((d) => ({
            x: new Date(d.timeStamp).getTime(),
            o: d.open,
            h: d.high,
            l: d.low,
            c: d.close,
          })),
        },
      ],
    };
    this.candleChartData = chartData;
    this.candleChartData = { ...this.candleChartData };
  }
}
