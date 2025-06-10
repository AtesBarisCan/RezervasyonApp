import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  email: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onLogin(): void {
  alert('TEST: onLogin() fonksiyonu tetiklendi!');
  this.authService.login(this.email, this.password).subscribe({
    next: () => {
      alert('🎉 Giriş başarılı! Ana sayfaya yönlendiriliyorsunuz...');
      this.router.navigate(['/anasayfa']);
    },
    error: (err) => {
      alert('❌ Giriş başarısız! Lütfen bilgileri kontrol edin.');
      console.error('Giriş hatası:', err);
    }
  });
  }

}
