using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class ArabaKira
    {
        public int Id { get; set; }
        public int PersonelId { get; set; }
        public int ArabaId { get; set; }
        public int KiraciId { get; set; }
        public int KiraFiyati { get; set; }
        public string Sure { get; set; }

        public virtual Araba Araba { get; set; }
        public virtual Kiraci Kiraci { get; set; }
        public virtual Personel Personel { get; set; }
    }
}
