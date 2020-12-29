using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class Araba
    {
        public Araba()
        {
            ArabaKiras = new HashSet<ArabaKira>();
            Fotografs = new HashSet<Fotograf>();
            IlanKoys = new HashSet<IlanKoy>();
            OzellikEkles = new HashSet<OzellikEkle>();
        }

        public int Id { get; set; }
        public int ArabaFirId { get; set; }
        public string Ad { get; set; }
        public int ServisId { get; set; }

        public virtual ArabaFirma ArabaFir { get; set; }
        public virtual ServisFirma Servis { get; set; }
        public virtual ICollection<ArabaKira> ArabaKiras { get; set; }
        public virtual ICollection<Fotograf> Fotografs { get; set; }
        public virtual ICollection<IlanKoy> IlanKoys { get; set; }
        public virtual ICollection<OzellikEkle> OzellikEkles { get; set; }
    }
}
