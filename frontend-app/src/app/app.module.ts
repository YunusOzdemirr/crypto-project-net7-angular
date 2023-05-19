import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgChartsModule } from 'ng2-charts';
import { HttpClientModule } from '@angular/common/http';
import { ApirequestComponent } from './components/apirequest/apirequest.component';
import { HttpClient } from '@angular/common/http';
import { CryptosComponent } from './components/cryptos/cryptos.component';

@NgModule({
  declarations: [
    AppComponent,
    ApirequestComponent,
    CryptosComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgChartsModule,
    HttpClientModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
