import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { MatPaginatorModule } from '@angular/material/paginator';



import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { HomepageComponent } from './components/homepage/homepage.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavBarComponent } from './components/nav-bar/nav-bar.component';
import { ProfileComponent } from './components/profile/profile.component';
import { RegisterComponent } from './components/register/register.component';
import { LoginComponent } from './components/login/login.component';
import { RecintosComponent } from './components/recintos/recintos.component';
import { RecintosShowComponent } from './components/recintos-show/recintos-show.component';
import { ReservasComponent } from './components/reservas/reservas.component';
import { MapsComponent } from './components/maps/maps.component';
import { RecintosDescriptionsComponent } from './components/recintos-descriptions/recintos-descriptions.component';
import { EditprofileComponent } from './components/editprofile/editprofile.component';
import { PagamentoComponent } from './components/pagamento/pagamento.component';
import { CommonModule, DatePipe } from '@angular/common';
import { ReserveHistoryComponent } from './components/reserve-history/reserve-history.component';
import { AvaliacoesComponent } from './components/avaliacoes/avaliacoes.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ConfirmarUtilizacaoPontosModalComponent } from './confirmar-utilizacao-pontos-modal/confirmar-utilizacao-pontos-modal.component';

@NgModule({
  declarations: [
    AppComponent,
    HomepageComponent,
    FooterComponent,
    NavBarComponent,
    ProfileComponent,
    RegisterComponent,
    LoginComponent,
    EditprofileComponent,
    LoginComponent,
    RecintosComponent,
    RecintosShowComponent,
    ReservasComponent,
    MapsComponent,
    RecintosDescriptionsComponent,
    PagamentoComponent,
    ReserveHistoryComponent,
    AvaliacoesComponent,
    ConfirmarUtilizacaoPontosModalComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MatPaginatorModule,
    BsDropdownModule,
    NgbModule
  ],
  providers: [DatePipe],
  bootstrap: [AppComponent]
})
export class AppModule { }
