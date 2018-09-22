import { Component, OnInit } from '@angular/core';
import { Sala,Usuario } from '../_models/index';
import { SalaService,BarraService } from '../_services/index';

@Component({
  templateUrl: './salaListado.component.html',
  styleUrls: ['./salaListado.component.css']
})
export class SalaListadoComponent implements OnInit {

  currentUser: Usuario;
  salas: Sala[] = [];
  constructor(private salaServicio: SalaService,public nav: BarraService) 
  { 
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  ngOnInit() {
    this.nav.show();
    this.listarTodasSalas();
  }

  eliminarSala(id: number) {
    this.salaServicio.delete(id).subscribe(() => { this.listarTodasSalas() });
  }

  private listarTodasSalas() {
      this.salaServicio.getAll().subscribe(rooms => { this.salas = rooms; });
  }
}
