import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginDTO } from '../models/loginDTO';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  readonly baseUrl = 'https://localhost:7777/api/users';
  private loginUrl = `${this.baseUrl}/login`;
  private registerUrl = `${this.baseUrl}/register`;
  private token: string | null = null;

  constructor() {
    
    this.token = localStorage.getItem('authToken');
  }

  login(credentials: LoginDTO): Observable<any> {
    return new Observable<any>(observer => {
      fetch(this.loginUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(credentials)
      })
      .then(response => response.json())
      .then(data => {
        console.log('Login response:', data);
        if (data?.result?.token) {
          this.setToken(data.result.token);
        } else {
          console.warn('⚠️ No se recibió un token válido:', data);
        }
        observer.next(data); 
        observer.complete(); 
      })
      .catch(error => {
        observer.error(error);
      });
    });
  }  

  loginAutomatico(): Observable<any> {
  const credentials: LoginDTO = {
    username: 'UsuarioAngular', // este debe existir en tu base de datos
    password: 'Usuario1234?'    // debe ser la correcta para ese usuario
  };

  return new Observable<any>(observer => {
    fetch(this.loginUrl, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(credentials)
    })
    .then(response => response.json())
    .then(data => {
      if (data?.result?.token) {
        this.setToken(data.result.token);
        observer.next(data);
        observer.complete();
      } else {
        observer.error(new Error('Token no recibido'));
      }
    })
    .catch(error => {
      console.error('Login automático falló:', error);
      observer.error(error);
    });
  });
}

  setToken(token: string): void {
    this.token = token;
    localStorage.setItem('authToken', token);
  }

  getToken(): string | null {
    return this.token || localStorage.getItem('authToken');
  }

  logout(): void {
    this.token = null;
    localStorage.removeItem('authToken');
  }
  
}