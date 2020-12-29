using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class IlanKoy
    {
        public int Id { get; set; }
        public int ArabaId { get; set; }
        public int PersonelId { get; set; }
        public DateTime Tarih { get; set; }
        public int Fiyat { get; set; }

        public virtual Araba Araba { get; set; }
        public virtual Personel Personel { get; set; }
    }
}
