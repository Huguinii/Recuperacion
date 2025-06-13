import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from './Auth.service';
import { FranjaHorariaModel } from '../models/franja-horaria-model';
import { ReservaModel } from '../models/reserva-model';
import { DiaNoLectivoModel } from '../models/dia-no-lectivo-model';




@Injectable({
  providedIn: 'root'
})
export class ApiService {

  private baseUrl = 'https://localhost:7777/api';

  constructor(private http: HttpClient, private authService: AuthService) { }

  private getAuthHeaders(): HttpHeaders {
    const token = this.authService.getToken();
    return new HttpHeaders({
      'Content-Type': 'application/json',
      ...(token ? { Authorization: `Bearer ${token}` } : {})
    });
  }

  getDiasNoLectivos(): Observable<DiaNoLectivoModel[]> {
    return this.http.get<DiaNoLectivoModel[]>(`${this.baseUrl}/DiaNoLectivo`, {
      headers: this.getAuthHeaders()
    });
  }

  getFranjasHorarias(): Observable<FranjaHorariaModel[]> {
    return this.http.get<FranjaHorariaModel[]>(`${this.baseUrl}/FranjaHoraria`, {
      headers: this.getAuthHeaders()
    });
  }

  getReservas(): Observable<ReservaModel[]> {
    return this.http.get<ReservaModel[]>(`${this.baseUrl}/Reserva`, {
      headers: this.getAuthHeaders()
    });
  }

  crearReserva(reserva: Partial<ReservaModel>): Observable<ReservaModel> {
    // Para crear una reserva, envía sólo los campos necesarios sin id ni fechaCreacion
    return this.http.post<ReservaModel>(`${this.baseUrl}/Reserva`, reserva, {
      headers: this.getAuthHeaders()
    });
  }

}
