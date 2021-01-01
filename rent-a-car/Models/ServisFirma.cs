using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class ServisFirma
    {
        public ServisFirma()
        {
            Arabas = new HashSet<Araba>();
        }

        public int Id { get; set; }
        public string Ad { get; set; }
        public string Servis { get; set; }

        public virtual ICollection<Araba> Arabas { get; set; }
    }
}
