import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AlertService, SalaService,BarraService } from '../_services/index';

@Component({
  templateUrl: './salaNueva.component.html',
  styleUrls: ['./salaNueva.component.css']
})
export class SalaNuevaComponent implements OnInit {
  model: any = {};
  constructor(
    private router: Router,
    private salaService: SalaService,
    private alertService: AlertService,
    public nav: BarraService
  ) { }

  ngOnInit() {
    this.nav.show();
  }

  Guardar() {
    this.salaService.create(this.model)
        .subscribe(
            data => {
                this.alertService.success('Sala Registrada', true);
                this.router.navigate(['/salaListado']);
            },
            error => {
                this.alertService.error(error._body);
            });
  }

}
