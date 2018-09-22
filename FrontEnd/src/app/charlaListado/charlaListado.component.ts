import { Component, OnInit } from '@angular/core';
import { Charla,Usuario } from '../_models/index';
import { CharlaService,BarraService } from '../_services/index';

@Component({
  templateUrl: './charlaListado.component.html',
  styleUrls: ['./charlaListado.component.css']
})
export class CharlaListadoComponent implements OnInit {
  currentUser: Usuario;
  charlas: Charla[] = [];
  constructor(private charlaServicio: CharlaService,public nav: BarraService) 
  { 
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  ngOnInit() {
    this.nav.show();
    this.listarTodasCharlas();
  }

  eliminarCharla(id: number) {
    this.charlaServicio.delete(id).subscribe(() => { this.listarTodasCharlas() });
  }

  private listarTodasCharlas() {
      this.charlaServicio.getAll().subscribe(c => { this.charlas = c; });
  }

}
