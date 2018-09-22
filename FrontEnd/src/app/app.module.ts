import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';

import { AppComponent } from './app.component';
import { AppRoutes } from './app.routing';
import { AppConfig } from './app.config';

import { AlertComponent } from './_directives/index';
import { AuthGuard } from './_guards/index';
import { AlertService, AuthenticationService,BarraService ,UsuarioService,SalaService,CharlaService,CharlaDetalleService} from './_services/index';
import { HomeComponent } from './home/index';
import { LoginComponent } from './login/index';
import { BarraMenuComponent } from './BarraMenu/BarraMenu.component';
import { UsuarioListadoComponent } from './usuarioListado/usuarioListado.component';
import { UsuarioNuevoComponent } from './usuarioNuevo/usuarioNuevo.component';
import { CharlaDetalle } from './_models';
import { SalaListadoComponent } from './salaListado/salaListado.component';
import { SalaNuevaComponent } from './salaNueva/salaNueva.component';
import { CharlaListadoComponent } from './charlaListado/charlaListado.component';
import { CharlaNuevaComponent } from './charlaNueva/charlaNueva.component';

@NgModule({
   declarations: [
      AppComponent,
      AlertComponent,
      HomeComponent,
      LoginComponent,
      BarraMenuComponent,
      UsuarioListadoComponent,
      UsuarioNuevoComponent,
      SalaListadoComponent,
      SalaNuevaComponent,
      CharlaListadoComponent,
      CharlaNuevaComponent
   ],
   imports: [
      BrowserModule,
      FormsModule,
      HttpModule,
      AppRoutes
   ],
   providers: [
      AppConfig,
      AuthGuard,
      AlertService,
      AuthenticationService,
      BarraService,
      UsuarioService,
      SalaService,
      CharlaService,
      CharlaDetalleService
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
