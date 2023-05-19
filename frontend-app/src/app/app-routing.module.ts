import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ApirequestComponent} from './components/apirequest/apirequest.component';
import {CryptosComponent} from "./components/cryptos/cryptos.component";


const routes: Routes = [
  {
    path: 'api',
    component: ApirequestComponent
  },
  {
    path: '',
    component: ApirequestComponent
  },
  {
    path: "cryptos",
    component: CryptosComponent
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})

export class AppRoutingModule {
}
