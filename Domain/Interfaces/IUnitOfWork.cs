using Domain.Core;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Repository<Drivers> Drivers { get;  }
        Repository<Infractions> Infractions { get; }
        Repository<InfractionsType> TypeInfractions { get; }
        Repository<Vehicles> Vehicle { get; }
        Repository<DriverInfractions> DriverInfractions { get; }
        Repository<VehiclesDriver> VehicleDrivers { get; }

        void SaveChanges();
    }
}
