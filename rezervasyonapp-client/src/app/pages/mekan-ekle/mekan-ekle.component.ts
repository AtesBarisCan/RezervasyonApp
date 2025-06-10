import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-mekan-ekle',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './mekan-ekle.component.html'
})
export class MekanEkleComponent {
  mekan = {
    ad: '',
    adres: '',
    aciklama: '',
    telefon: '',
    eposta: '',
    konumLinki: '',
    kullaniciId: 0
  };

  selectedFile: File | null = null;

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      this.selectedFile = file;
    }
  }

  constructor(private http: HttpClient, private router: Router) {}

  uploadGorsel(): Promise<string> {
    const formData = new FormData();
    formData.append('dosya', this.selectedFile!);

    console.log("📦 Gönderilen formData içeriği:");
    formData.forEach((value, key) => {
    console.log(`${key}:`, value);
  });

    const token = localStorage.getItem('token');
    const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

    return this.http.post<any>('https://localhost:7224/api/mekan/gorsel-yukle', formData, { headers }).toPromise()
      .then(res => res.url);
  }

  async mekanEkle() {
  const token = localStorage.getItem('token');
  const headers = new HttpHeaders().set('Authorization', `Bearer ${token}`);

  const formData = new FormData();
  formData.append('ad', this.mekan.ad);
  formData.append('adres', this.mekan.adres);
  formData.append('aciklama', this.mekan.aciklama);
  formData.append('telefon', this.mekan.telefon);
  formData.append('eposta', this.mekan.eposta);
  formData.append('googleMapsLink', this.mekan.konumLinki); // Modeldeki adı bu mu kontrol et

  if (this.selectedFile) {
    try {
      const gorselUrl = await this.uploadGorsel();
      formData.append("gorselUrl", gorselUrl);
    } catch (err) {
      console.error("Görsel yüklenemedi", err);
      return;
    }
  }

  console.log('📝 Gönderilen form verisi:');
  formData.forEach((v, k) => console.log(`${k}:`, v));

  this.http.post('https://localhost:7224/api/mekan', formData, { headers }).subscribe({
    next: () => {
      alert('Mekan başarıyla eklendi!');
      this.router.navigate(['/anasayfa']);
    },
    error: err => {
      console.error('❌ Hata:', err);
    }
  });
}




  ngOnInit(): void {
    const token = localStorage.getItem('token');
    console.log("TOKEN:", token);
    if (token) {
      try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        console.log('📦 Token payload:', payload);
      } catch (err) {
        console.error('❌ Token çözümleme hatası:', err);
      }
    } else {
      console.warn('⚠️ Token yok.');
    }
  }
}


