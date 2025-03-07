import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { HomepageComponent } from './components/homepage/homepage.component';
import { ProfileComponent } from './components/profile/profile.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { RecintosComponent } from './components/recintos/recintos.component'
import { RecintosShowComponent } from './components/recintos-show/recintos-show.component'
import { EditprofileComponent } from './components/editprofile/editprofile.component';
import { PagamentoComponent } from './components/pagamento/pagamento.component';
import { ReservasComponent } from './components/reservas/reservas.component';
import { ReserveHistoryComponent } from './components/reserve-history/reserve-history.component';
 

const routes: Routes = [
  {
    path: "",
    component: HomepageComponent,
  },
  {
    path: "login",
    component: LoginComponent,
  },
  {
    path: "register",
    component: RegisterComponent,
  },
  {
    path: "profile/:id",
    component: ProfileComponent,
  },
  {
    path: "recintos",
    component: RecintosComponent,
  },
  {
    path: "recintos/:id",
    component: RecintosShowComponent,
  },
  {
    path: "profile/edit",
    component: EditprofileComponent,
  },
  {
    path: "pagamento",
    component: PagamentoComponent,
  },

  {
    path: "reservas",
    component: ReservasComponent,
  },

  {
    path: "reserveHistory",
    component: ReserveHistoryComponent,
  }

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
