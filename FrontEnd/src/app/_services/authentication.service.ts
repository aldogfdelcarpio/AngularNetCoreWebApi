import { Injectable } from '@angular/core';
import{ Http,Headers,Response } from '@angular/http';
import { map } from 'rxjs/operators';
// import 'rxjs/add/operator/map'
import { AppConfig } from '../app.config';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: Http, private config: AppConfig) { }

  login(correo: string, password: string) 
  {
    return this.http.post(this.config.apiUrl + '/Usuarios/Login', { Correo: correo, password: password })
        .pipe(map((response: Response) => {
            // login successful if there's a jwt token in the response
            let user = response.json();
            if (user && user.token) {
                // store user details and jwt token in local storage to keep user logged in between page refreshes
                localStorage.setItem('currentUser', JSON.stringify(user));
            }
        }));
  }

  logout()
  {
    localStorage.removeItem('currentUser');
  }
}
