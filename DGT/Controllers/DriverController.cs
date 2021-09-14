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
    public class DriverController : Controller
    {
        private IUnitOfWork _uow;

        public DriverController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Add new driver
        /// </summary>
        /// <param name="conductor"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public IActionResult Add(Drivers model)
        {

            bool exists = _uow.Drivers.Single(x => x.Dni.Trim() == model.Dni.Trim()) != null;
            if (exists)
                return BadRequest("DNI " + model.Dni + " already exists");

            _uow.Drivers.Add(model);
            _uow.SaveChanges();
            return Ok(model);
        }

        /// <summary>
        /// Get drivers
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetDrivers(int count)
        {
            var data = _uow.Drivers.All().Take(count).ToList();
            return Ok(data);
        }
    }
}
