import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({ providedIn: 'root' })
export class IsletmeGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate(): boolean {
    const token = localStorage.getItem('token');
    const rol = localStorage.getItem('userRole');

    if (token && rol === 'Isletme') {
      return true;
    }

    alert('Bu sayfaya sadece işletme sahipleri erişebilir!');
    this.router.navigate(['/anasayfa']);
    return false;
  }
}
