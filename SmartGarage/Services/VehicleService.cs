using SmartGarage.Models;
using SmartGarage.Models.DTOs;
using SmartGarage.Exceptions;
using SmartGarage.Repositories;
using SmartGarage.Models.DTOs.VehicleDTO;

namespace SmartGarage.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository vehicleRepository;

    public Vehicle CreateVehicle(VehicleCreateDTO dto)
    {
        var vehicle = new Vehicle(dto.CarMake, dto.CarModel, dto.CarVin, dto.CarYear, dto.CarLicencePlate, dto.CarSystemId);

        return vehicleRepository.CreateNewVehicle(vehicle) ?? throw new Exception("Vehicle creating failed.");
    }

    public string DeleteVehicle(int id)
    {
        var detele = vehicleRepository.DeleteVehicle(id);

        return id; // ne znam zashto iska da se konvertira 
    }

    public List<Vehicle> GetAllVehicles()

    {
        var vehicles = vehicleRepository.GetAllVehicles();

        return vehicles ?? throw new EntityNotFoundException("There are no registred cars in the system.");
    }

    public Vehicle GetVehicleById(int id)
    {
        var vehicle = vehicleRepository.GetVehicleById(id);

        return vehicle ?? throw new EntityNotFoundException($"Vehicle with id: '{id}' is not found.");
    }

    public Vehicle UpdateVehicle(int id, VehicleUpdateDTO dto)
    {
        var vehicle = new Vehicle(dto.CarMake, dto.CarModel, dto.CarVin, dto.CarYear, dto.CarLicencePlate, dto.CarSystemId);
        var update = vehicleRepository.UpdateVehicle(id, vehicle);

        if ( update == null)
        {
            throw new EntityNotFoundException($"Vehicle with such id: {id} is not found");
        }
        return update;
    }
}