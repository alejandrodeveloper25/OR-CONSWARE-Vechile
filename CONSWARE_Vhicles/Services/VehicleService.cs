using System;
using CONSWARE_Vhicles.Data;
using CONSWARE_Vhicles.Dtos;
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
                .Columns("name, creationDate, updateDate, active")
                .Values($"'{vehicle.name}', NOW(), NOW(), 1")
                .Build();

         

            _dataAccess.Execute(query);

            int lastInsertedId = _dataAccess.GetLastInsertedId();

            return this.GetVehicleById(lastInsertedId);
        }

        // Método para obtener todos los vehículos
        public IEnumerable<Vehicle> GetAllVehicles()
        {
            var query = new QueryBuilder()
                .Select("*")
                .From("vehicle")
                .Build();

            var vehicles = _dataAccess.Query<Vehicle>(query);

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

            return vehicle;
        }


        //Método para obtener los vehiculos que cumplen con los criterios de busqueda
        public IEnumerable<Vehicle> GetAllVehiclesByCriteria(CriteriaDTO cirteriaDTO) {
            var vehicles = _dataAccess.ExecuteStoredProcedure<Vehicle>("PRO_GetAllVehiclesByCriteria", cirteriaDTO);

            return vehicles;
        }

        // Método para actualizar un vehículo existente
        public Vehicle UpdateVehicle(Vehicle vehicle)
        {
            var query = new QueryBuilder()
                .Update("vehicle")
                .Set($"name = '{vehicle.name}', creationDate = '{vehicle.creationDate.ToString("yyyy-MM-dd HH:mm:ss")}', updateDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', active = {Convert.ToInt32(vehicle.active)}")
                .Where($"idVehicle = {vehicle.idVehicle}")
                .Build();

            _dataAccess.Execute(query);

            return vehicle;
        }
    }
}

