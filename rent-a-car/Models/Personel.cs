using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class Personel
    {
        public Personel()
        {
            ArabaKiras = new HashSet<ArabaKira>();
            IlanKoys = new HashSet<IlanKoy>();
        }

        public int Id { get; set; }
        public int OfisId { get; set; }

        public virtual User IdNavigation { get; set; }
        public virtual Ofi Ofis { get; set; }
        public virtual ICollection<ArabaKira> ArabaKiras { get; set; }
        public virtual ICollection<IlanKoy> IlanKoys { get; set; }
    }
}
