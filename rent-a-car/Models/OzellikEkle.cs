using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class OzellikEkle
    {
        public int Id { get; set; }
        public int ArabaId { get; set; }
        public int OzellikId { get; set; }
        public DateTime Tarih { get; set; }

        public virtual Araba Araba { get; set; }
        public virtual Ozellik Ozellik { get; set; }
    }
}
