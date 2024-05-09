using System;
using CONSWARE_Vhicles.Data;
using CONSWARE_Vhicles.Models;

namespace CONSWARE_Vhicles.Services
{
    public class VehicleService
    {
        private readonly DataAccess _dataAccess;

        public VehicleService(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Método para crear un nuevo vehículo
        public Vehicle CreateVehicle(Vehicle vehicle)
        {
            var query = new QueryBuilder()
                .InsertInto("vehicle")
                .Columns("idVehicleType, name, creationDate, updateDate, active")
                .Values($"{vehicle.idVehicleType}, {vehicle.name}, '{vehicle.creationDate.ToString("yyyy-MM-dd HH:mm:ss")}', '{vehicle.updateDate.ToString("yyyy-MM-dd HH:mm:ss")}', {Convert.ToInt32(vehicle.active)}")
                .Build();

            _dataAccess.Execute(query);

            // Obtener el último ID insertado
            int lastInsertedId = _dataAccess.GetLastInsertedId();

            // Asignar el ID generado al objeto Vehicle
            vehicle.idVehicle = lastInsertedId;

            return vehicle;
        }

        // Método para obtener todos los vehículos
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            var query = new QueryBuilder()
                .Select("*")
                .From("vehicle")
                .Build();

            var vehicles = _dataAccess.Query<Vehicle>(query);

            // Para cada vehículo, obtener el tipo de vehículo correspondiente
            foreach (var vehicle in vehicles)
            {
                var vehicleTypeQuery = new QueryBuilder()
                    .Select("*")
                    .From("vehicle_type")
                    .Where($"idVehicleType = {vehicle.idVehicleType}")
                    .Build();

                var vehicleType = _dataAccess.QueryFirstOrDefault<VehicleType>(vehicleTypeQuery);
                vehicle.vehicleType = vehicleType;
            }

            return vehicles;
        }

        // Método para obtener un vehículo por su Id
        public Vehicle GetVehicleById(int id)
        {
            var query = new QueryBuilder()
                .Select("*")
                .From("vehicle")
                .Where($"idVehicle = {id}")
                .Build();

            var vehicle = _dataAccess.QueryFirstOrDefault<Vehicle>(query);

            // Obtener el tipo de vehículo correspondiente
            if (vehicle != null)
            {
                var vehicleTypeQuery = new QueryBuilder()
                    .Select("*")
                    .From("vehicle_type")
                    .Where($"idVehicleType = {vehicle.idVehicleType}")
                    .Build();

                var vehicleType = _dataAccess.QueryFirstOrDefault<VehicleType>(vehicleTypeQuery);
                vehicle.vehicleType = vehicleType;
            }

            return vehicle;
        }

        // Método para actualizar un vehículo existente
        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            var query = new QueryBuilder()
                .Update("vehicle")
                .Set($"idVehicleType = {vehicle.idVehicleType}, name = {vehicle.name}, creationDate = '{vehicle.creationDate.ToString("yyyy-MM-dd HH:mm:ss")}', updateDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', active = {Convert.ToInt32(vehicle.active)}")
                .Where($"idVehicle = {vehicle.idVehicle}")
                .Build();

            _dataAccess.Execute(query);

            return vehicle;
        }
    }
}

