import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';

import { AppConfig } from '../app.config';
import { CharlaDetalle } from '../_models/index';
import { map } from 'rxjs/operators';

@Injectable()
export class CharlaDetalleService {
    constructor(private http: Http, private config: AppConfig) { }

    getAll() {
        return this.http.get(this.config.apiUrl + '/CharlasDetalle', this.jwt()).pipe(map((response: Response) => response.json()));
    }

    getById(id: number) {
        return this.http.get(this.config.apiUrl + '/CharlasDetalle/' + id, this.jwt()).pipe(map((response: Response) => response.json()));
    }

    getMisCharlas(id: number) {
        return this.http.get(this.config.apiUrl + '/CharlasDetalle/MisCharlas/' + id, this.jwt()).pipe(map((response: Response) => response.json()));
    }

    getCharlasDisponibles(id: number) {
        return this.http.get(this.config.apiUrl + '/CharlasDetalle/CharlasDisponibles/' + id, this.jwt()).pipe(map((response: Response) => response.json()));
    }

    create(charlaDetalle: CharlaDetalle) {
        return this.http.post(this.config.apiUrl + '/CharlasDetalle', charlaDetalle, this.jwt());
    }

    delete(idAsistente: number,idCharla: number) {
        return this.http.delete(this.config.apiUrl + '/CharlasDetalle/' + idAsistente + "/" + idCharla, this.jwt());
    }

    private jwt() {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.token) {
            let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
            return new RequestOptions({ headers: headers });
        }
    }

}
