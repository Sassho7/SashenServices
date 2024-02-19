using SmartGarage.Data;
using SmartGarage.Exceptions;
using SmartGarage.Models;
using SmartGarage.Models.DTOs;
using SmartGarage.Services;
using Microsoft.EntityFrameworkCore;

namespace SmartGarage.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly SmartGarageDbContext context;

    public VehicleRepository(SmartGarageDbContext context)
    {
        this.context = context;
    }

    public Vehicle? CreateNewVehicle(Vehicle vehicle)
    {
        context.Vehicles.Add(vehicle);
        var save = context.SaveChanges() > 0;

        return save ? vehicle : null;
    }

    public bool DeleteVehicle(int id)
    {
        var vehicleToRemove = GetVehicleById(id);

        if ( vehicleToRemove == null)
        {
            return false;
        }

        context.Vehicles.Remove(vehicleToRemove);
        var save = context.SaveChanges() > 0;

        return save;
    }

    public List<Vehicle> GetAllVehicles()
    {
        return GetVehicles().ToList();
    }

    public Vehicle? GetVehicleById(int id)
    {
        var vehicle = GetVehicles().FirstOrDefault(v => v.Id == id);

        return vehicle; 
    }

    public bool MechanicToVehicle(Mechanic mechanic, Vehicle vehicle)
    {
        // oshe ne mogada izmislq tochno kak da stane ili s try catch blok ili s promenliva i ot bazata da vadi
        throw new NotImplementedException();
    }

    public Vehicle UpdateVehicle(int id, Vehicle vehicle)
    {
        var vehicleToUpdate = GetVehicleById(id);

        if (vehicleToUpdate == null)
        {
            return null;
        }

        vehicleToUpdate.CarMake = vehicle.CarMake;
        vehicleToUpdate.CarModel = vehicle.CarModel;
        vehicleToUpdate.CarYear = vehicle.CarYear;
        vehicleToUpdate.CarVin = vehicle.CarVin;
        vehicleToUpdate.CarLicencePlate = vehicle.CarLicencePlate;
        vehicleToUpdate.MechanicToVehicle = vehicle.MechanicToVehicle;

        context.Update(vehicleToUpdate);
        context.SaveChanges();

        return vehicleToUpdate;
    }

    private IQueryable<Vehicle> GetVehicles()
    {
        return context.Vehicles
            .Include(vehicle => vehicle.CarMake)
            .Include(vehicle => vehicle.CarModel)
            .Include(vehcile => vehcile.CarYear)
            .Include(Vehicle => Vehicle.CarVin)
            .Include(vehicle => vehicle.CarLicencePlate);
    }
}