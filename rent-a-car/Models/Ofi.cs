using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class Ofi
    {
        public Ofi()
        {
            Personels = new HashSet<Personel>();
        }

        public int Id { get; set; }

        public virtual ICollection<Personel> Personels { get; set; }
    }
}
