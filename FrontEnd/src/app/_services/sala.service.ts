import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';

import { AppConfig } from '../app.config';
import { Sala } from '../_models/index';
import { map } from 'rxjs/operators';

@Injectable()
export class SalaService {
    constructor(private http: Http, private config: AppConfig) { }

    getAll() {
        return this.http.get(this.config.apiUrl + '/Salas', this.jwt()).pipe(map((response: Response) => response.json()));
    }

    getById(id: number) {
        return this.http.get(this.config.apiUrl + '/Salas/' + id, this.jwt()).pipe(map((response: Response) => response.json()));
    }

    create(sala: Sala) {
        return this.http.post(this.config.apiUrl + '/Salas', sala, this.jwt());
    }

    update(sala: Sala) {
        return this.http.put(this.config.apiUrl + '/Sala/' + sala.id, sala, this.jwt());
    }

    delete(id: number) {
        return this.http.delete(this.config.apiUrl + '/Sala/' + id, this.jwt());
    }

    private jwt() {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.token) {
            let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
            return new RequestOptions({ headers: headers });
        }
    }

}
