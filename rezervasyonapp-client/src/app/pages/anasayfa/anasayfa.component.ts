import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Mekan } from '../../models/mekan.model';
import { RouterModule } from '@angular/router';
@Component({
  selector: 'app-anasayfa',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './anasayfa.component.html',
  styleUrls: ['./anasayfa.component.css']
})
export class AnasayfaComponent implements OnInit {
  userRole: string | null = null;
  mekanlar: Mekan[] = [];

  constructor(private http: HttpClient, private router: Router) {}

  ngOnInit(): void {
    this.userRole = localStorage.getItem('userRole');
    this.getTumu().subscribe(data => this.mekanlar = data);
  }

  getTumu(): Observable<Mekan[]> {
    return this.http.get<Mekan[]>('https://localhost:7224/api/mekan');
  }

  mekanDetay(id: number): void {
    if (this.userRole === 'Musteri') {
      this.router.navigate(['/mekan', id]);
    } else {
      alert("Detayları görebilmek için bireysel hesapla giriş yapmalısınız.");
    }
  }

  uyelikGerekli(): void {
    alert("Lütfen giriş yaparak devam edin.");
  }
}
