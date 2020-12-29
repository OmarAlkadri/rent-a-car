using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class User
    {
        public User()
        {
            Logins = new HashSet<Login>();
        }

        public int Id { get; set; }
        public int Password { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string Email { get; set; }
        public string KisiTuru { get; set; }

        public virtual Admin Admin { get; set; }
        public virtual Personel Personel { get; set; }
        public virtual ICollection<Login> Logins { get; set; }
    }
}
