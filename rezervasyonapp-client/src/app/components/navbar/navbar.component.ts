import { Component } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './navbar.component.html',
})
export class NavbarComponent {
  constructor(private router: Router) {}

  get isLogged(): boolean {
    return !!localStorage.getItem('token');
  }

  get isIsletme(): boolean {
    return localStorage.getItem('userRole') === 'Isletme';
  }

  logout(): void {
    localStorage.clear();
    this.router.navigate(['/giris']);
  }
}
