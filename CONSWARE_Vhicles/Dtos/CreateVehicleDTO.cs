using System;
using CONSWARE_Vhicles.Models;

namespace CONSWARE_Vhicles.Dtos
{
	public class CreateOrUpdateVehicleDTO
	{
        public string name { get; set; }
        public int idVehicleType { get; set; }
        public int idFuelType { get; set; }
        public string brand { get; set; }
        public int model { get; set; }
        public decimal cylinderCapacity { get; set; }
        public int seating { get; set; }
        public bool automatic { get; set; }
        public bool manual { get; set; }
        public decimal price { get; set; }
    }
}
