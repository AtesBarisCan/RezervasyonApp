using System;
using System.Text.Json.Serialization;

namespace RezervasyonApp.Models
{
    public class Rezervasyon
    {
        public int Id { get; set; }

        public int KullaniciId { get; set; }

        [JsonIgnore]
        public Kullanici? Kullanici { get; set; }

        public int MekanId { get; set; }

        [JsonIgnore]
        public Mekan? Mekan { get; set; }

        public DateTime Tarih { get; set; }
        public int KisiSayisi { get; set; }

        public RezervasyonDurumu Durum { get; set; }

    }
}
