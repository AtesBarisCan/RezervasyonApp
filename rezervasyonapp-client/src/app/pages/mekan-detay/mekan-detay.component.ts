import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { Mekan } from '../../models/mekan.model'; // doğru path olduğuna emin ol
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-mekan-detay',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mekan-detay.component.html',
})
export class MekanDetayComponent implements OnInit {
  mekan!: Mekan;
  userRole: string | null = null;

  constructor(
    private route: ActivatedRoute,
    private http: HttpClient,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.userRole = localStorage.getItem('userRole');

    const mekanId = this.route.snapshot.paramMap.get('id');
    if (mekanId) {
      this.http
        .get<Mekan>(`https://localhost:7224/api/mekan/${mekanId}`)
        .subscribe({
          next: (data) => {
            this.mekan = data;
          },
          error: (err) => {
            console.error('Mekan alınamadı:', err);
            alert('Mekan bulunamadı.');
            this.router.navigate(['/anasayfa']);
          },
        });
    }
  }

  rezervasyonYap() {
    // örnek işlem – ileri aşamada yönlendirme yapılabilir
    alert('Rezervasyon işlemine yönlendiriliyorsunuz...');
    this.router.navigate([`/rezervasyon/${this.mekan.id}`]);
  }
}

