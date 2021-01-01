using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class Kiraci
    {
        public Kiraci()
        {
            ArabaKiras = new HashSet<ArabaKira>();
        }

        public int Id { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public int Yas { get; set; }

        public virtual ICollection<ArabaKira> ArabaKiras { get; set; }
    }
}
