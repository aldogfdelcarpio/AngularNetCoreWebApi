import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService, CharlaService,BarraService,SalaService } from '../_services/index';

@Component({
  templateUrl: './charlaNueva.component.html',
  styleUrls: ['./charlaNueva.component.css']
})
export class CharlaNuevaComponent implements OnInit {
  model: any = {};
  listaSalas: any;  
  listaSpeakers: any;
  constructor(
    private router: Router,
    private charlaService: CharlaService,
    private alertService: AlertService,
    private salaService: SalaService,
    public nav: BarraService
  ) 
  {
    this.obtenerListaSalas();
    this.obtenerListaSpeakers();
  }

  ngOnInit() {
    this.nav.show();
  }

  obtenerListaSalas(){
    this.salaService.getAll().subscribe(data => {
      this.listaSalas = data;
    });
  }

  obtenerListaSpeakers(){
    this.charlaService.getSpeakers().subscribe(data => {
      this.listaSpeakers = data;
    });
  }

  Guardar() {
    this.charlaService.create(this.model)
        .subscribe(
            data => {
                this.alertService.success('Charla Registrada', true);
                this.router.navigate(['/charlaListado']);
            },
            error => {
                this.alertService.error(error._body);
            });
  }

}
