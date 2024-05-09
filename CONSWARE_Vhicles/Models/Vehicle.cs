using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSWARE_Vhicles.Models
{
	public class Vehicle
	{
        public int idVehicle { get; set; }

        public string name { get; set; }

        public DateTime creationDate { get; set; }

        public DateTime updateDate { get; set; }

        public bool active { get; set; }
    }
}

