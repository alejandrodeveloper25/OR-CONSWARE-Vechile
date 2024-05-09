using System;
using CONSWARE_Vhicles.Data;
using CONSWARE_Vhicles.Models;

namespace CONSWARE_Vhicles.Services
{
    public class VehicleTypeService
    {
        private readonly DataAccess _dataAccess;

        public VehicleTypeService(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Método para crear un nuevo tipo de vehículo
        public VehicleType CreateVehicleType(VehicleType vehicleType)
        {
            var query = new QueryBuilder()
                .InsertInto("vehicle_type")
                .Columns("name, code, creation_date, update_date, active")
                .Values($"'{vehicleType.name}', '{vehicleType.code}', '{vehicleType.creationDate.ToString("yyyy-MM-dd HH:mm:ss")}', '{vehicleType.updateDate.ToString("yyyy-MM-dd HH:mm:ss")}', {Convert.ToInt32(vehicleType.active)}")
                .Build();

            _dataAccess.Execute(query);

            // Obtener el último ID insertado
            int lastInsertedId = _dataAccess.GetLastInsertedId();

            // Asignar el ID generado al objeto VehicleType
            vehicleType.idVehicleType = lastInsertedId;

            return vehicleType;
        }

        // Método para obtener todos los tipos de vehículo
        public IEnumerable<VehicleType> GetAllVehicleTypes()
        {
            var query = new QueryBuilder()
                .Select("*")
                .From("vehicle_type")
                .Build();

            return _dataAccess.Query<VehicleType>(query);
        }

        // Método para obtener un tipo de vehículo por su Id
        public VehicleType GetVehicleTypeById(int id)
        {
            var query = new QueryBuilder()
                .Select("*")
                .From("vehicle_type")
                .Where($"idVehicleType = {id}")
                .Build();

            return _dataAccess.QueryFirstOrDefault<VehicleType>(query);
        }

        // Método para actualizar un tipo de vehículo existente
        public VehicleType UpdateVehicleType(VehicleType vehicleType)
        {
            var query = new QueryBuilder()
                .Update("vehicle_type")
                .Set($"name = '{vehicleType.name}', code = '{vehicleType.code}', creationDate = '{vehicleType.creationDate.ToString("yyyy-MM-dd HH:mm:ss")}', updateDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', active = {Convert.ToInt32(vehicleType.active)}")
                .Where($"idVehicleType = {vehicleType.idVehicleType}")
                .Build();

            _dataAccess.Execute(query);

            return vehicleType;
        }

    }
}

