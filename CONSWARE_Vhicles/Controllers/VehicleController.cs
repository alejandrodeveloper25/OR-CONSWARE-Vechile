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

        [HttpPost("criteria")]
        public IActionResult GetAllVehiclesByCriteria(CriteriaDTO criteriaDTO)
        {
            var vehicles = _vehicleService.GetAllVehiclesByCriteria(criteriaDTO);
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

            var newVehicle = new Vehicle();
            var newVechileDetails = new VehicleDetails();

            newVehicle.name = createVehicleDTO.name;

            var vehicle = _vehicleService.CreateVehicle(newVehicle);
            if (vehicle == null)
            {
                return NotFound();
            }

            newVechileDetails.idVehicle = vehicle.idVehicle;
            newVechileDetails.idFuelType = createVehicleDTO.idFuelType;
            newVechileDetails.idVehicleType = createVehicleDTO.idVehicleType;
            newVechileDetails.brand = createVehicleDTO.brand;
            newVechileDetails.model = createVehicleDTO.model;
            newVechileDetails.cylinderCapacity = createVehicleDTO.cylinderCapacity;
            newVechileDetails.seating = createVehicleDTO.seating;
            newVechileDetails.automatic = createVehicleDTO.automatic;
            newVechileDetails.manual = createVehicleDTO.manual;
            newVechileDetails.price = createVehicleDTO.price;

            var vehicleDetails = _vehicleDetailsService.CreateVehicleDetails(newVechileDetails);
            if (vehicleDetails == null)
            {
                return NotFound();
            }

            return Ok(vehicle);
        }

        [HttpPut]
        public IActionResult EditVechile([FromQuery] int id, [FromBody] CreateOrUpdateVehicleDTO updateVehicleDTO)
        {

            var vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
            {
                return NotFound();
            }

            var vehicleDetails = _vehicleDetailsService.GetVehicleDetailsByIdVechile(vehicle.idVehicle);

            vehicleDetails.idVehicleType = updateVehicleDTO.idVehicleType;
            vehicleDetails.idFuelType = updateVehicleDTO.idFuelType;
            vehicleDetails.brand = updateVehicleDTO.brand;
            vehicleDetails.model = updateVehicleDTO.model;
            vehicleDetails.cylinderCapacity = updateVehicleDTO.cylinderCapacity;
            vehicleDetails.seating = updateVehicleDTO.seating;
            vehicleDetails.automatic = updateVehicleDTO.automatic;
            vehicleDetails.manual = updateVehicleDTO.manual;
            vehicleDetails.price = updateVehicleDTO.price;

            vehicleDetails = _vehicleDetailsService.UpdateVehicleDetails(vehicleDetails);

            vehicle.name = updateVehicleDTO.name;
            vehicle.updateDate = new DateTime();

            vehicle = _vehicleService.UpdateVehicle(vehicle);

            return Ok(vehicle);
        }

        [HttpPost("{id}/delete")]
        public IActionResult deleteVehicle(int id)
        {
            var vehicle = _vehicleService.GetVehicleById(id);
            if (vehicle == null)
            {
                return NotFound();
            }


            vehicle.updateDate = new DateTime();
            vehicle.active = false;

            vehicle = _vehicleService.UpdateVehicle(vehicle);

            return Ok(vehicle);
        }
    }
}

