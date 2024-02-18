using SmartGarage.Models;
using SmartGarage.Models.DTOs;
using SmartGarage.Models.DTOs.VehicleDTO;

namespace SmartGarage.Services;

public interface IVehicleService
{
    public List<Vehicle> GetAllVehicles();
    public Vehicle GetVehicleById(int id);
    public Vehicle CreateVehicle(VehicleCreateDTO dto);
    public Vehicle UpdateVehicle(int id, VehicleUpdateDTO dto);
    public string DeleteVehicle(int id);
}