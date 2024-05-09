using System;
using CONSWARE_Vhicles.Models;
using CONSWARE_Vhicles.Services;
using Microsoft.AspNetCore.Mvc;

namespace CONSWARE_Vhicles.Controllers
{
    [ApiController]
    [Route("api/config")]
    public class ConfigurationController : ControllerBase
    {
        private readonly FuelTypeService _fuelTypeService;
        private readonly VehicleTypeService _vehicleTypeService;

        public ConfigurationController(FuelTypeService fuelTypeService, VehicleTypeService vehicleTypeService)
        {
            _fuelTypeService = fuelTypeService;
            _vehicleTypeService = vehicleTypeService;
        }

        [HttpGet("fuel_type")]
        public IActionResult GetAllFuelTypes()
        {
            var fuelTypes = _fuelTypeService.GetAllFuelTypes();
            return Ok(fuelTypes);
        }

        [HttpGet("fuel_type/{id}")]
        public IActionResult GetFuelTypeById(int id)
        {
            var fuelType = _fuelTypeService.GetFuelTypeById(id);
            if (fuelType == null)
            {
                return NotFound();
            }

            return Ok(fuelType);
        }

        [HttpGet("vehicle-type")]
        public ActionResult<IEnumerable<VehicleType>> GetAllVehicleTypes()
        {
            var vehicleTypes = _vehicleTypeService.GetAllVehicleTypes();
            return Ok(vehicleTypes);
        }

        [HttpGet("vehicle-type/{id}")]
        public ActionResult<VehicleType> GetVehicleType(int id)
        {
            var vehicleType = _vehicleTypeService.GetVehicleTypeById(id);
            if (vehicleType == null)
            {
                return NotFound();
            }
            return Ok(vehicleType);
        }
    }
}

