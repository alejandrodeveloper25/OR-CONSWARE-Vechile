using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSWARE_Vhicles.Models
{
	public class VehicleDetails
	{
        public int idVehicleDetails { get; set; }

        public int idVehicle { get; set; }

        public int idFuelType { get; set; }
        public FuelType fuelType { get; set; }

        public string brand { get; set; }

        public int model { get; set; }

        public float cylinderCapacity { get; set; }

        public int seating { get; set; }

        public bool automatic { get; set; }

        public bool manual { get; set; }

        public float price { get; set; }

        public string urlImg { get; set; }

        public DateTime creationDate { get; set; }

        public DateTime updateDate { get; set; }

        public bool active { get; set; }
    }
}

