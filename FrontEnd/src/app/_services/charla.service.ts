import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';

import { AppConfig } from '../app.config';
import { Charla } from '../_models/index';
import { map } from 'rxjs/operators';

@Injectable()
export class CharlaService {
    constructor(private http: Http, private config: AppConfig) { }

    getAll() {
        return this.http.get(this.config.apiUrl + '/Charlas', this.jwt()).pipe(map((response: Response) => response.json()));
    }

    getSpeakers() {
        return this.http.get(this.config.apiUrl + '/Charlas/ListarSpeakers', this.jwt()).pipe(map((response: Response) => response.json()));
    }

    getById(id: number) {
        return this.http.get(this.config.apiUrl + '/Charlas/' + id, this.jwt()).pipe(map((response: Response) => response.json()));
    }

    create(charla: Charla) {
        return this.http.post(this.config.apiUrl + '/Charlas', charla, this.jwt());
    }

    update(charla: Charla) {
        return this.http.put(this.config.apiUrl + '/Charlas/' + charla.id, charla, this.jwt());
    }

    delete(id: number) {
        return this.http.delete(this.config.apiUrl + '/Charlas/' + id, this.jwt());
    }

    private jwt() {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.token) {
            let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
            return new RequestOptions({ headers: headers });
        }
    }
}
