using Domain.Interfaces;
using Models;
using Models.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Core
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _context;
        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        private Repository<Drivers> _drivers;
        private Repository<Infractions> _infractions;
        private Repository<InfractionsType> _typeInfractions;
        private Repository<Vehicles> _vehicle;
        private Repository<DriverInfractions> _driverInfractions;
        private Repository<VehiclesDriver> _vehiclesDriver;



        public Repository<Drivers> Drivers => _drivers ?? new Repository<Drivers>(_context);

        public Repository<Infractions> Infractions => _infractions ?? new Repository<Infractions>(_context);

        public Repository<InfractionsType> TypeInfractions => _typeInfractions ?? new Repository<InfractionsType>(_context);

        public Repository<Vehicles> Vehicle => _vehicle ?? new Repository<Vehicles>(_context);

        public Repository<DriverInfractions> DriverInfractions => _driverInfractions ?? new Repository<DriverInfractions>(_context);

        public Repository<VehiclesDriver> VehicleDrivers => _vehiclesDriver ?? new Repository<VehiclesDriver>(_context);



        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
