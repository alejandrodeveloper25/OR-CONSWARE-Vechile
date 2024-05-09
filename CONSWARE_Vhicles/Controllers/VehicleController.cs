using System;
using CONSWARE_Vhicles.Dtos;
using CONSWARE_Vhicles.Models;
using CONSWARE_Vhicles.Services;
using Microsoft.AspNetCore.Mvc;

namespace CONSWARE_Vhicles.Controllers
{
    [ApiController]
    [Route("api/vehicle")]
    public class VehicleController : ControllerBase
    {
        private readonly VehicleService _vehicleService;
        private readonly VehicleDetailsService _vehicleDetailsService;

        public VehicleController(VehicleService vehicleService, VehicleDetailsService vehicleDetailsService)
        {
            _vehicleService = vehicleService;
            _vehicleDetailsService = vehicleDetailsService;
        }

        [HttpGet]
        public IActionResult GetAllVehicles()
        {
            var vehicles = _vehicleService.GetAllVehicles();
            return Ok(vehicles);
        }

        [HttpGet("{id}")]
        public IActionResult GetVehicleByID(int id)
        {
            var vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        [HttpGet("{id}/details")]
        public IActionResult GetVehicleDetailsByIdVechile(int id)
        {
            var vehicleDetails = _vehicleDetailsService.GetVehicleDetailsByIdVechile(id);
            if (vehicleDetails == null)
            {
                return NotFound();
            }

            return Ok(vehicleDetails);
        }

        [HttpPost]
        public IActionResult createVechile([FromBody] CreateOrUpdateVehicleDTO createVehicleDTO)
        {
            var vehicle = _vehicleService.CreateVehicle(createVehicleDTO.Vehicle);
            if (vehicle == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        [HttpPut("{id}")]
        public IActionResult createVechile([FromQuery] int id, [FromBody] CreateOrUpdateVehicleDTO updateVehicleDTO)
        {
            var vechicleUpdate = _vehicleService.UpdateVehicle(updateVehicleDTO.Vehicle);
            if (vechicleUpdate == null)
            {
                return NotFound();
            }

            return Ok(vechicleUpdate);
        }

        [HttpPost("{id}")]
        public IActionResult deleteVehicle([FromQuery] int id)
        {

           

            return Ok("");
        }
    }
}

