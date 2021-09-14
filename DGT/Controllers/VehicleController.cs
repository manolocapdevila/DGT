using DGT.ModelosVM;
using Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DGT.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : Controller
    {
        private IUnitOfWork _uow;

        public VehicleController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Add new vehicle
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult Add(VehicleVM model)
        {

            bool exists = _uow.Vehicle.Filter(x => x.RegistrationCode.Trim() == model.RegistrationCode.Trim()).ToList().Count > 0;
            if (exists)
                return BadRequest("Registration number" + model.RegistrationCode + " already exists");

            if (model.CasualDriverDni.Count == 0)
                return BadRequest("Is neccesario a regular driver DNI");

            List<int> lstIdDrivers = new List<int>();
            foreach (var dni in model.CasualDriverDni)
            {
                var existsDriver = _uow.Drivers.Single(x => x.Dni.Equals(dni.Trim()));
                if (existsDriver == null)
                    return BadRequest("The driver " + dni + " doesn't exists");

                var totalRegular = _uow.VehicleDrivers.Filter(x => x.IdDriver == existsDriver.Id).ToList().Count;
                if (totalRegular > 10)
                    return BadRequest("The driver " + dni + " can not be regular in more than 10 vehicles");


                lstIdDrivers.Add(existsDriver.Id);
            }


            

            // Esto debería ir en transacción
            var v = new Vehicles();
            v.RegistrationCode = model.RegistrationCode;
            v.Brand = model.Brand;
            v.Model = model.Model;

            _uow.Vehicle.Add(v);

            _uow.SaveChanges();

            foreach(var id in lstIdDrivers)
            {
                var vd = new VehiclesDriver();
                vd.IdDriver = id;
                vd.IdVehicle = v.Id;
                _uow.VehicleDrivers.Add(vd);
            }

            _uow.SaveChanges();

            return Ok(model);
        }
    }
}
