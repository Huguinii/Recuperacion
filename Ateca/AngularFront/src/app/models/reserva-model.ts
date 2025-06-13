export interface ReservaModel {
  id: string;
  estado: 'Aprobada' | 'Pendiente' | 'Rechazada';
  fechaCreacion: string;  // Ejemplo: "2025-05-29T23:56:54.6137512"
  fecha: string;          // Ejemplo: "2025-05-29"
  horaInicio: string;     // Ejemplo: "12:00:00"
  horaFin: string;        // Ejemplo: "13:00:00"
  grupo: string;
  nombreProfesor: string;
}