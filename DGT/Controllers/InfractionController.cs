using DGT.ModelosVM;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
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
    public class InfractionController : Controller
    {

        private readonly IUnitOfWork _uow;

        public InfractionController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Add new infraction
        /// </summary>
        /// <param name="infraccion"></param>
        /// <returns></returns>        
        [HttpPost("[action]")]
        public IActionResult Add(Infractions model)
        {

            _uow.Infractions.Add(model);
            _uow.SaveChanges();
            return Ok(model);
        }

        /// <summary>
        /// Register infraction to a driver
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult RegisterInfraction(InfraccionVehiculoVM model)
        {
            var v = _uow.Vehicle.GetById(model.IdVehicule);
            var i = _uow.Infractions.GetById(model.IdInfraction);
            var vd = _uow.VehicleDrivers.Filter(x => x.IdVehicle == v.Id).First();
            var d = _uow.Drivers.GetById(vd.IdDriver);

            // Los puntos de la infracción se guardan en esta tabla. Ya que los puntos a descontar pueden variar con el tiempo
            var di = new DriverInfractions();
            di.Date = DateTime.Now;
            di.IdDriver = vd.IdDriver;
            di.IdInfraction = i.Id;
            di.Points = i.PointsDiscount;

            // Ojo, los puntos pueden ser negativos
            d.Points -= i.PointsDiscount;

            _uow.DriverInfractions.Add(di);
            _uow.Drivers.Update(d);

            _uow.SaveChanges();

            return Ok(model);
        }

        /// <summary>
        /// Get all infractions from driver
        /// </summary>
        /// <param name="IdDriver"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetDriverInfractions(string dni)
        {
            var driver = _uow.Drivers.Single(x => x.Dni.Equals(dni.Trim()));
            var di = _uow.DriverInfractions.Filter(x => x.IdDriver == driver.Id);

            var data = _uow.Infractions.All().Join(di,
                                                    left => left.Id,
                                                    right => right.IdInfraction,
                                                    (left, right) => new { Code = left.Code, Description = left.Description, Points = right.Points, Date = right.Date }).ToList();
                                                
            return Ok(data);
        }

        /// <summary>
        /// Get 5 common infractions type
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetCommonInfractions()
        {
            var di = _uow.DriverInfractions.All().GroupBy(x => new { x.IdInfraction })
                                            .Select(y => new { IdInfraction = y.Key.IdInfraction, Count = y.Count() })
                                            .OrderByDescending(z => z.Count)
                                            .ToList();

            var i = _uow.Infractions.All().ToList();

            var data = di.Join(i,
                               left => left.IdInfraction,
                               right => right.Id,
                               (left, right) => new { Code = right.Code, Description = right.Description, Total = left.Count}).Take(5).ToList();

            return Ok(data);
        }

        [HttpGet("[action]")]
        public IActionResult GetInfractionTypes()
        {
            var data = _uow.TypeInfractions.All();
            return Ok(data);
        }
    }
}
