using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Models
{
    public partial class Infractions
    {
        public int Id { get; set; }
        public int IdInfractionType { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal PointsDiscount { get; set; }
    }
}
