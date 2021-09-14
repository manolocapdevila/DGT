using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Models
{
    public partial class Vehicles
    {
        public int Id { get; set; }
        public string RegistrationCode { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
    }
}
