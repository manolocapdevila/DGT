using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGT.ModelosVM
{
    public class VehicleVM
    {
        public string RegistrationCode { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }

        public List<string> CasualDriverDni { get; set; }
    }
}
