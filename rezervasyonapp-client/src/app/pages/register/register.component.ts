import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService, UserRole } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
  name = '';
  email = '';
  password = '';
  role: UserRole | '' = '';

  constructor(private authService: AuthService, private router: Router) {}

  onRegister(): void {
    const userData = {
      ad: this.name,
      soyad: '', 
      eposta: this.email,
      sifre: this.password,
      rol: this.role
    };

    this.authService.register(userData).subscribe({
      next: () => {
        alert('Kayıt başarılı! Giriş sayfasına yönlendiriliyorsunuz...');
        this.router.navigate(['/giris']);
      },
      error: (err) => {
        console.error('Kayıt hatası:', err);
        alert('Kayıt başarısız! Lütfen bilgileri kontrol edin.');
      }
    });
  }
}
