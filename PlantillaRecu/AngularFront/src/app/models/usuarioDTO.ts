export interface usuarioDTO {
  id: string;            // UUID o identificador único
  nombre: string;
  edad: number;
  correo: string;
  rol?: string;          // opcional: admin, profesor, etc.
  activo?: boolean;      // opcional: indica si el usuario está habilitado
}
