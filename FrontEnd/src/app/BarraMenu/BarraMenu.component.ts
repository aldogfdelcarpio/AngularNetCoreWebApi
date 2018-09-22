import { Component } from '@angular/core';
import { BarraService } from '../_services/index';

@Component({
  selector: 'app-BarraMenu',
  templateUrl: './BarraMenu.component.html',
  styleUrls: ['./BarraMenu.component.css']
})
export class BarraMenuComponent {

  constructor(public nav: BarraService) { }

  ngOnInit() {
  }

}
