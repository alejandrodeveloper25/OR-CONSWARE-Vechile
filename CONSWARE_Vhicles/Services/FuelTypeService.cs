using System;
using CONSWARE_Vhicles.Data;
using CONSWARE_Vhicles.Models;

namespace CONSWARE_Vhicles.Services
{
    public class FuelTypeService
    {
        private readonly DataAccess _dataAccess;

        public FuelTypeService(DataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Método para crear un nuevo tipo de combustible
        public FuelType CreateFuelType(FuelType fuelType)
        {
            var query = new QueryBuilder()
                .InsertInto("fuel_type")
                .Columns("name, code, creation_date, update_date, active")
                .Values($"'{fuelType.name}', '{fuelType.code}', '{fuelType.creationDate.ToString("yyyy-MM-dd HH:mm:ss")}', '{fuelType.updateDate.ToString("yyyy-MM-dd HH:mm:ss")}', {Convert.ToInt32(fuelType.active)}")
                .Build();

            _dataAccess.Execute(query);

            // Obtener el último ID insertado
            int lastInsertedId = _dataAccess.GetLastInsertedId();

            // Asignar el ID generado al objeto FuelType
            fuelType.idFuelType = lastInsertedId;

            return fuelType;
        }

        // Método para obtener todos los tipos de combustible
        public IEnumerable<FuelType> GetAllFuelTypes()
        {
            var query = new QueryBuilder()
                .Select("*")
                .From("fuel_type")
                .Build();

            return _dataAccess.Query<FuelType>(query);
        }

        // Método para obtener un tipo de combustible por su Id
        public FuelType GetFuelTypeById(int id)
        {
            var query = new QueryBuilder()
                .Select("*")
                .From("fuel_type")
                .Where($"idFuelType = {id}")
                .Build();

            return _dataAccess.QueryFirstOrDefault<FuelType>(query);
        }

        // Método para actualizar un tipo de combustible existente
        public FuelType UpdateFuelType(FuelType fuelType)
        {
            var query = new QueryBuilder()
                .Update("fuel_type")
                .Set($"name = '{fuelType.name}', code = '{fuelType.code}', creationDate = '{fuelType.creationDate.ToString("yyyy-MM-dd HH:mm:ss")}', updateDate = '{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}', active = {Convert.ToInt32(fuelType.active)}")
                .Where($"idFuelType = {fuelType.idFuelType}")
                .Build();

            _dataAccess.Execute(query);

            return fuelType;
        }

    }
}

