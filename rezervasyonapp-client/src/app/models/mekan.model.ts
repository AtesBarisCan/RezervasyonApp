export interface Kullanici{
    id: number;
    ad: string;
    soyad: string;
}

export interface Mekan{
    id: number;
    ad: string;
    adres: string;
    telefon: string;
    eposta: string;
    aciklama: string;
    konumLinki?: string;
    gorselUrl?: string;
    puan?: number;
    kullaniciId: number;
    kullanici: Kullanici;
}