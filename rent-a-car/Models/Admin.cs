using System;
using System.Collections.Generic;

#nullable disable

namespace rent_a_car
{
    public partial class Admin
    {
        public int Id { get; set; }

        public virtual User IdNavigation { get; set; }
    }
}
