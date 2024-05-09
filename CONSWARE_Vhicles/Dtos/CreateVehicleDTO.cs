using System;
using CONSWARE_Vhicles.Models;

namespace CONSWARE_Vhicles.Dtos
{
	public class CreateOrUpdateVehicleDTO
	{
        public Vehicle Vehicle { get; set; }
        public VehicleDetails VehicleDetails { get; set; }
    }
}

