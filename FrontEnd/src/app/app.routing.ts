import { Routes, RouterModule } from '@angular/router';

import { HomeComponent } from './home/index';
import { LoginComponent } from './login/index';
import { UsuarioListadoComponent } from './usuarioListado/usuarioListado.component';
import { UsuarioNuevoComponent } from './usuarioNuevo/usuarioNuevo.component';
import { SalaListadoComponent } from './salaListado/salaListado.component';
import { SalaNuevaComponent } from './salaNueva/salaNueva.component';
import { CharlaListadoComponent } from './charlaListado/charlaListado.component';
import { CharlaNuevaComponent } from './charlaNueva/charlaNueva.component';
import { AuthGuard } from './_guards/index';

const routes: Routes = [
  { path: '', component: HomeComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent },
  { path: 'usuarioListado', component: UsuarioListadoComponent },
  { path: 'usuarioNuevo', component: UsuarioNuevoComponent },
  { path: 'salaListado', component: SalaListadoComponent },
  { path: 'salaNueva', component: SalaNuevaComponent },
  { path: 'charlaListado', component: CharlaListadoComponent },
  { path: 'charlaNueva', component: CharlaNuevaComponent },
  { path: '**', redirectTo: '' }
];

export const AppRoutes = RouterModule.forRoot(routes);
