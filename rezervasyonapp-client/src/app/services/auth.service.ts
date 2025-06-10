import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { Router } from '@angular/router';

export enum UserRole {
  Bireysel = 'Bireysel',
  Isletme = 'Isletme'
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7224/api/auth'; 
  private userRoleSubject = new BehaviorSubject<UserRole | null>(null);
  public userRole$ = this.userRoleSubject.asObservable();

  constructor(private http: HttpClient, private router: Router) {
    const savedRole = localStorage.getItem('userRole') as UserRole;
    if (savedRole) this.userRoleSubject.next(savedRole);
  }

  login(email: string, password: string): Observable<any> {
  return this.http.post(`${this.apiUrl}/login`, { email, password }).pipe(
    tap((res: any) => {
      const token = res.token;
      const payload = JSON.parse(atob(token.split('.')[1]));

      const role = payload["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"]; // ✅ bu satır önemli

      localStorage.setItem('token', token);
      localStorage.setItem('userName', res.ad);
      localStorage.setItem('userRole', role); // ✅ doğru yerden alınan rol
      localStorage.setItem('userId', payload.nameid);

      console.log("✅ Giriş başarılı:", payload);
    })
  );
}





  register(data: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/register`, data);
  }

  logout(): void {
    localStorage.clear();
    this.userRoleSubject.next(null);
    this.router.navigate(['/giris']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('token');
  }

  getUserRole(): UserRole | null {
    return localStorage.getItem('userRole') as UserRole | null;
  }
}
