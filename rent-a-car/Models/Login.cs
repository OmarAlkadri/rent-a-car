using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class Login
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Tarih { get; set; }

        public virtual User User { get; set; }
    }
}
