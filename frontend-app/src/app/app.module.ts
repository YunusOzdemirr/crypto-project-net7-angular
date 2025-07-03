import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgChartsModule } from 'ng2-charts';
import { HttpClientModule } from '@angular/common/http';
import { ApirequestComponent } from './components/apirequest/apirequest.component';
import { CryptosComponent } from './components/cryptos/cryptos.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatTableModule } from '@angular/material/table';
import { CandleComponent } from './components/candle/candle.component';
import { CoinDetailComponent } from './components/coin-detail/coin-detail.component';

@NgModule({
  declarations: [
    AppComponent,
    ApirequestComponent,
    CryptosComponent,
    CandleComponent,
    CoinDetailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    MatTableModule,
    NgChartsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }