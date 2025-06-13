// src/app/services/reserva.service.ts
import { Injectable } from '@angular/core';
import { CrearReservaModel } from '../models/crear-reserva-model';
import { ReservaModel } from '../models/reserva-model';
import { FranjaHorariaModel } from '../models/franja-horaria-model';
import { DiaNoLectivoModel } from '../models/dia-no-lectivo-model';


@Injectable({
  providedIn: 'root'
})
export class ReservaService {
  private readonly baseUrl = 'https://localhost:7777/api/';

  constructor() {}

  private getAuthHeaders(): { [key: string]: string } {
  const token = localStorage.getItem('token');
  return {
    'Authorization': `Bearer ${token}`,
    'Content-Type': 'application/json'
  };
}


async crearReserva(dto: CrearReservaModel): Promise<ReservaModel> {
  const diasNoLectivos = await this.getAllDiaNoLectivos();

  const formatDate = (date: Date): string =>
    date.toLocaleDateString('en-CA'); // Formato YYYY-MM-DD sin desfase

  const fechaReserva = formatDate(new Date(dto.fecha));

  console.log('Fecha de la reserva:', fechaReserva);
  console.log('Días no lectivos:', diasNoLectivos.map(d => formatDate(new Date(d.fecha))));

  const esDiaNoLectivo = diasNoLectivos.some(dia =>
    formatDate(new Date(dia.fecha)) === fechaReserva
  );

  if (esDiaNoLectivo) {
    throw new Error('No se puede crear una reserva en un día no lectivo.');
  }

  const formatHora = (hora: string) =>
    hora.length === 5 ? `${hora}:00` : hora;

  const dtoFormateado: CrearReservaModel = {
    ...dto,
    horaInicio: formatHora(dto.horaInicio),
    horaFin: formatHora(dto.horaFin),
  };

  const response = await fetch(`${this.baseUrl}Reserva`, {
    method: 'POST',
    headers: this.getAuthHeaders(),
    body: JSON.stringify(dtoFormateado),
  });

  if (!response.ok) {
    const mensajeError = await response.text();
    console.error('Error al crear reserva:', mensajeError);
    throw new Error(mensajeError || 'Error al crear la reserva');
  }

  return await response.json();
}



  async getAllReservas(): Promise<ReservaModel[]> {
  const response = await fetch(`${this.baseUrl}Reserva`, {
    method: 'GET',
    headers: this.getAuthHeaders()
  });

  if (!response.ok) {
    const errorText = await response.text();
    console.error("Error al cargar reservas:", errorText);
    throw new Error("Error al obtener las reservas.");
  }

  return (await response.json()) ?? [];
}

  async getAllFranjasHorarias(): Promise<FranjaHorariaModel[]> {
  const response = await fetch(`${this.baseUrl}FranjaHoraria/disponibles`, {
    method: 'GET',
    headers: this.getAuthHeaders()
  });

  if (!response.ok) {
    const errorText = await response.text();
    console.error("Error al cargar franjas horarias:", errorText);
    throw new Error("Error al obtener las franjas horarias.");
  }

  return (await response.json()) ?? [];
}

  async getAllDiaNoLectivos(): Promise<DiaNoLectivoModel[]> {
  const response = await fetch(`${this.baseUrl}DiaNoLectivo`, {
    method: 'GET',
    headers: this.getAuthHeaders()
  });
  if (!response.ok) {
    const errorText = await response.text();
    console.error("Error al cargar días no lectivos:", errorText);
    throw new Error("Error al obtener los días no lectivos.");
  }
  return (await response.json()) ?? [];
}

  async eliminarReserva(id: string): Promise<boolean> {
    const response = await fetch(`${this.baseUrl}Reserva/${id}`, {
      method: 'DELETE',
      headers: this.getAuthHeaders()
    });

    if (!response.ok) {
      const mensajeError = await response.text();
      console.error(`Error al eliminar reserva ${id}:`, mensajeError);
    }

    return response.ok;
  }
}
