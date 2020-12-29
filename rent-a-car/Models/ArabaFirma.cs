using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class ArabaFirma
    {
        public ArabaFirma()
        {
            Arabas = new HashSet<Araba>();
        }

        public int Id { get; set; }
        public string Ad { get; set; }
        public string FirmaSahibi { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public int AdresId { get; set; }

        public virtual Adre Adres { get; set; }
        public virtual ICollection<Araba> Arabas { get; set; }
    }
}
