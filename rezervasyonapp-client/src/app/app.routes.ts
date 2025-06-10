import { Routes } from '@angular/router';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { AnasayfaComponent } from './pages/anasayfa/anasayfa.component';
import { AuthGuard } from './guards/auth.guard';
import { MekanDetayComponent } from './pages/mekan-detay/mekan-detay.component';
import { RezervasyonComponent } from './pages/rezervasyon/rezervasyon.component';
import { MekanEkleComponent } from './pages/mekan-ekle/mekan-ekle.component';
import { IsletmeGuard } from './guards/isletme.guard';

export const routes: Routes = [
  { path: '', redirectTo: 'anasayfa', pathMatch: 'full' }, // ilk açılışta anasayfa
  { path: 'anasayfa', component: AnasayfaComponent },       // herkese açık
  { path: 'mekan/:id', component: MekanDetayComponent },    // herkese açık
  { path: 'rezervasyon/:id', component: RezervasyonComponent, canActivate: [AuthGuard] }, // sadece giriş yapan
  { path: 'giris', component: LoginComponent },
  { path: 'kayit', component: RegisterComponent },
  { path: 'mekan-ekle', component: MekanEkleComponent, canActivate: [IsletmeGuard] }

];
