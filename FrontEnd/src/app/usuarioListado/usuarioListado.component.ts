import { Component, OnInit } from '@angular/core';
import { Usuario } from '../_models/index';
import { UsuarioService,BarraService } from '../_services/index';

@Component({
  templateUrl: './usuarioListado.component.html',
  styleUrls: ['./usuarioListado.component.css']
})
export class UsuarioListadoComponent implements OnInit {

  currentUser: Usuario;
  usuarios: Usuario[] = [];
  constructor( private usuarioServicio: UsuarioService,public nav: BarraService ) 
  {
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
   }

  ngOnInit() {
    this.nav.show();
    this.listarTodosUsuarios();
  }

  eliminarUsuario(id: number) {
    this.usuarioServicio.delete(id).subscribe(() => { this.listarTodosUsuarios() });
  }

  private listarTodosUsuarios() {
      this.usuarioServicio.getAll().subscribe(users => { this.usuarios = users; });
  }

}
