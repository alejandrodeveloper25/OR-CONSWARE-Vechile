using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CONSWARE_Vhicles.Models
{
	public class FuelType
	{
        public int idFuelType { get; set; }

        public string name { get; set; }

        public string code { get; set; }

        public DateTime creationDate { get; set; }

        public DateTime updateDate { get; set; }

        public bool active { get; set; }
    }
}

