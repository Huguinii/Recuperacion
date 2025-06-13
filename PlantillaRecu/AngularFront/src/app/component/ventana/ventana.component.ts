import { Component, signal } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { usuarioDTO } from '../../models/usuarioDTO';

@Component({
  selector: 'app-ventana',
  standalone: true,
  imports: [NgIf, NgFor],
  templateUrl: './ventana.component.html',
  styleUrls: ['./ventana.component.css'],
})
export class VentanaComponent {
  usuarios = signal<usuarioDTO[]>([
    {
      id: '1',
      nombre: 'Juan Pérez',
      edad: 30,
      correo: 'juan@example.com',
      rol: 'profesor',
      activo: true,
    },
    {
      id: '2',
      nombre: 'Ana García',
      edad: 25,
      correo: 'ana@example.com',
      rol: 'admin',
      activo: false,
    },
  ]);

  mensaje = signal('');
}
