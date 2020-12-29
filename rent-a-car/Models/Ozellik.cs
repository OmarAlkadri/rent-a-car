using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class Ozellik
    {
        public Ozellik()
        {
            OzellikEkles = new HashSet<OzellikEkle>();
        }

        public int Id { get; set; }
        public char OzellikTipi { get; set; }

        public virtual ICollection<OzellikEkle> OzellikEkles { get; set; }
    }
}
