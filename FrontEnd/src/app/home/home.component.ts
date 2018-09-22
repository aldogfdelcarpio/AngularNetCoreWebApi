import { Component, OnInit } from '@angular/core';
import { Usuario,Charla,CharlaDetalle } from '../_models/index'
import { BarraService,CharlaDetalleService } from '../_services/index'
// import { jsonpFactory } from '@angular/http/src/http_module';

@Component({
  // selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  currentUser: Usuario;
  charlasDisponibles: Charla[] = [];
  charlasMias: Charla[] = [];
  charlaDetalle: any = {};
  constructor(public nav: BarraService,private charlaServicio: CharlaDetalleService) 
  { 
    this.currentUser = JSON.parse(localStorage.getItem('currentUser'));
  }

  ngOnInit() {
    this.nav.show();
    this.listarCharlasDisponibles(this.currentUser.id);
    this.listarCharlasMias(this.currentUser.id);
  }

  private listarCharlasDisponibles(id: number) {
    this.charlaServicio.getCharlasDisponibles(id).subscribe(charlas => { this.charlasDisponibles = charlas; });
  }

  private listarCharlasMias(id: number) {
    this.charlaServicio.getMisCharlas(id).subscribe(charlas => { this.charlasMias = charlas; });
  }

  eliminarCharla(idCharla: number) {
    this.charlaServicio.delete(this.currentUser.id,idCharla).subscribe(() => { 
      this.listarCharlasDisponibles(this.currentUser.id);
      this.listarCharlasMias(this.currentUser.id);
     });
  }

  incribirseCharla(id:number){      
    this.charlaDetalle.idAsistente = this.currentUser.id;
    this.charlaDetalle.idCharla = id;
    this.charlaServicio.create(this.charlaDetalle).subscribe(() => { 
      this.listarCharlasDisponibles(this.currentUser.id);
      this.listarCharlasMias(this.currentUser.id);
     });
  }

}
