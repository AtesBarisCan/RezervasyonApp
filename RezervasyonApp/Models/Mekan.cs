namespace RezervasyonApp.Models
{
    public class Mekan
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public string Adres { get; set; }
        public string Telefon { get; set; }
        public string Eposta { get; set; }
        public string Aciklama { get; set; }
        public string? GorselUrl { get; set; }
        public string? KonumLinki { get; set; }

        public int KullaniciId {  get; set; }
        public Kullanici? Kullanici { get; set; }   
    }
}
