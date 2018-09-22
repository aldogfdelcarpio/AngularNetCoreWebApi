import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService, UsuarioService,BarraService } from '../_services/index';

@Component({
  templateUrl: './usuarioNuevo.component.html',
  styleUrls: ['./usuarioNuevo.component.css']
})
export class UsuarioNuevoComponent implements OnInit {
  model: any = {};
  listaTipoUsuario: any;  
  constructor(
    private router: Router,
    private userService: UsuarioService,
    private alertService: AlertService,
    public nav: BarraService
  ) {
    this.obtenerListaUsuario();
   }

  ngOnInit() {
    this.nav.show();
  }

  obtenerListaUsuario(){
    this.userService.getTipoUsuario().subscribe(data => {
      this.listaTipoUsuario = data;
    });
  }

  Guardar() {
    this.userService.create(this.model)
        .subscribe(
            data => {
                this.alertService.success('Usuario Registrado', true);
                this.router.navigate(['/usuarioListado']);
            },
            error => {
                this.alertService.error(error._body);
            });
  }

}
