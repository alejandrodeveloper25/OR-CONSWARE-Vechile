using System;
using CONSWARE_Vhicles.Data;
using CONSWARE_Vhicles.Models;

namespace CONSWARE_Vhicles.Services
{
    public class VehicleDetailsService
    {
        private readonly DataAccess _dataAccess;

        public VehicleDetailsService(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Método para crear detalles de vehículo
        public VehicleDetails CreateVehicleDetails(VehicleDetails vehicleDetails)
        {
            var query = new QueryBuilder()
                .InsertInto("vehicle_details")
                .Columns("idVehicle, idFuelType, brand, model, cylinderCapacity, seating, automatic, manual, price, urlImg, creationDate, updateDate, active")
                .Values($"{vehicleDetails.idVehicle}, {vehicleDetails.idFuelType}, '{vehicleDetails.brand}', {vehicleDetails.model}, {vehicleDetails.cylinderCapacity}, {vehicleDetails.seating}, {Convert.ToInt32(vehicleDetails.automatic)}, {Convert.ToInt32(vehicleDetails.manual)}, {vehicleDetails.price}, {vehicleDetails.urlImg}, '{vehicleDetails.creationDate.ToString("yyyy-MM-dd HH:mm:ss")}', '{vehicleDetails.updateDate.ToString("yyyy-MM-dd HH:mm:ss")}', {Convert.ToInt32(vehicleDetails.active)}")
                .Build();

            _dataAccess.Execute(query);

            // Obtener el último ID insertado
            int lastInsertedId = _dataAccess.GetLastInsertedId();

            // Asignar el ID generado al objeto VehicleDetails
            vehicleDetails.idVehicleDetails = lastInsertedId;

            return vehicleDetails;
        }

        // Método para obtener todos los detalles de vehículo
        public IEnumerable<VehicleDetails> GetAllVehicleDetails()
        {
            var query = new QueryBuilder()
                .Select("*")
                .From("vehicle_details")
                .Build();

            var vehicleDetails = _dataAccess.Query<VehicleDetails>(query);

            // Para cada detalle de vehículo, obtener el tipo de combustible correspondiente
            foreach (var detail in vehicleDetails)
            {
                var fuelTypeQuery = new QueryBuilder()
                    .Select("*")
                    .From("fuel_type")
                    .Where($"idFuelType = {detail.idFuelType}")
                    .Build();

                var fuelType = _dataAccess.QueryFirstOrDefault<FuelType>(fuelTypeQuery);
                detail.fuelType = fuelType;
            }

            return vehicleDetails;
        }

        // Método para obtener detalles de vehículo por idVehicle
        public VehicleDetails GetVehicleDetailsByIdVechile(int id)
        {
            var query = new QueryBuilder()
                .Select("*")
                .From("vehicle_details")
                .Where($"idVehicle = {id}")
                .Build();

            var vehicleDetails = _dataAccess.QueryFirstOrDefault<VehicleDetails>(query);

            // Obtener el tipo de combustible correspondiente
            if (vehicleDetails != null)
            {
                var fuelTypeQuery = new QueryBuilder()
                    .Select("*")
                    .From("fuel_type")
                    .Where($"idFuelType = {vehicleDetails.idFuelType}")
                    .Build();

                var fuelType = _dataAccess.QueryFirstOrDefault<FuelType>(fuelTypeQuery);
                vehicleDetails.fuelType = fuelType;
            }

            return vehicleDetails;
        }

        // Método para actualizar detalles de vehículo existentes
        public VehicleDetails UpdateVehicleDetails(VehicleDetails vehicleDetails)
        {
            var query = new QueryBuilder()
                .Update("vehicle_details")
                .Set($"idVehicle = {vehicleDetails.idVehicle}, idFuelType = {vehicleDetails.idFuelType}, brand = '{vehicleDetails.brand}', model = {vehicleDetails.model}, cylinderCapacity = {vehicleDetails.cylinderCapacity}, seating = {vehicleDetails.seating}, automatic = {Convert.ToInt32(vehicleDetails.automatic)}, manual = {Convert.ToInt32(vehicleDetails.manual)}, price = {vehicleDetails.price}, urlImg= {vehicleDetails.urlImg}, creationDate = '{vehicleDetails.creationDate.ToString("yyyy-MM-dd HH:mm:ss")}', updateDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', active = {Convert.ToInt32(vehicleDetails.active)}")
                .Where($"idVehicleDetails = {vehicleDetails.idVehicleDetails}")
                .Build();

            _dataAccess.Execute(query);

            return vehicleDetails;
        }
    }
}

