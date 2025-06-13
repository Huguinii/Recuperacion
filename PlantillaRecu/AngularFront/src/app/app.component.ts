import { Component, OnInit } from '@angular/core';
import { AuthService } from './service/Auth.service';
import { Router } from '@angular/router';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterModule],
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent implements OnInit {
  constructor(
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    const token = this.authService.getToken();
    
    if (token) {
      console.log('🔓 Token ya presente en localStorage');
      this.router.navigate(['/principal']);
    } else {
      this.authService.loginAutomatico().subscribe({
        next: () => {
          console.log('✅ Login automático exitoso');
          this.router.navigate(['/principal']);
        },
        error: err => console.error('❌ Error en login automático:', err)
      });
    }
  }
}
