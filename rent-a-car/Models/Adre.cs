using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class Adre
    {
        public Adre()
        {
            ArabaFirmas = new HashSet<ArabaFirma>();
        }

        public int Id { get; set; }
        public string Ilce { get; set; }
        public string Il { get; set; }
        public string Satir1 { get; set; }
        public string Satir2 { get; set; }

        public virtual ICollection<ArabaFirma> ArabaFirmas { get; set; }
    }
}
