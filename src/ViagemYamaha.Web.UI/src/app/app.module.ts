import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { HttpClientModule  } from '@angular/common/http';
import { FormsModule } from '@angular/forms';


import { AppComponent } from './app.component';

import { RotaService } from '../app/services/rota.service';
import { RotasComponent } from './components/rotas/rotas.component';
import { AdicionarrotaComponent } from './components/adicionarrota/adicionarrota.component';
import { DefaultComponent } from './components/default/default.component';

@NgModule({
  declarations: [
    AppComponent,
    RotasComponent,
    AdicionarrotaComponent,
    DefaultComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule
  ],
  providers: [RotaService],
  bootstrap: [AppComponent]
})
export class AppModule { }
