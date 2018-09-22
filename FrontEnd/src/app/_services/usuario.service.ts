import { Injectable } from '@angular/core';
import { Http, Headers, RequestOptions, Response } from '@angular/http';

import { AppConfig } from '../app.config';
import { Usuario } from '../_models/index';
import { map } from 'rxjs/operators';

@Injectable()
export class UsuarioService {
    constructor(private http: Http, private config: AppConfig) { }

    getAll() {
        return this.http.get(this.config.apiUrl + '/Usuarios', this.jwt()).pipe(map((response: Response) => response.json()));
    }

    getById(id: number) {
        return this.http.get(this.config.apiUrl + '/Usuarios/' + id, this.jwt()).pipe(map((response: Response) => response.json()));
    }

    create(user: Usuario) {
        return this.http.post(this.config.apiUrl + '/Usuarios', user, this.jwt());
    }

    update(user: Usuario) {
        return this.http.put(this.config.apiUrl + '/Usuarios/' + user.id, user, this.jwt());
    }

    delete(id: number) {
        return this.http.delete(this.config.apiUrl + '/Usuarios/' + id, this.jwt());
    }

    getTipoUsuario(){
        return this.http.get(this.config.apiUrl + '/Usuarios/ListarTipoUsuario', this.jwt()).pipe(map((response: Response) => response.json()));
    }


    private jwt() {
        let currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.token) {
            let headers = new Headers({ 'Authorization': 'Bearer ' + currentUser.token });
            return new RequestOptions({ headers: headers });
        }
    }
}
