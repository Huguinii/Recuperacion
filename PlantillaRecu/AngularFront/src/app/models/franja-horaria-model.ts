export interface FranjaHorariaModel {
  id: string;
  horaInicio: string;  // Ejemplo: "00:00:00"
  horaFin: string;     // Ejemplo: "01:00:00"
  esRecreo: boolean;
  disponible: boolean;
}