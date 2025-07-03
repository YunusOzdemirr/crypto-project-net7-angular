import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ApirequestComponent } from './components/apirequest/apirequest.component';
import { CryptosComponent } from './components/cryptos/cryptos.component';
import { CoinDetailComponent } from './components/coin-detail/coin-detail.component';

const routes: Routes = [
  { path: '', component: CryptosComponent },
  { path: 'coin/:symbol', component: CoinDetailComponent },
  { path: 'api', component: ApirequestComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }