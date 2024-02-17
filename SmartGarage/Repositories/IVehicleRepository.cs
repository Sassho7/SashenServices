using Humanizer.Localisation.TimeToClockNotation;
using SmartGarage.Models;
using SmartGarage.Models.DTOs;

namespace SmartGarage.Repositories;

public interface IVehicleRepository
{
    List<Vehicle> GetAllVehicles();
    Vehicle? GetVehicleById(int id); //moje da vurne null stoinost
    Vehicle? CreateNewVehicle(Vehicle vehicle); // validation failure or unhendeld exception
    Vehicle UpdateVehicle(int id, Vehicle vehicle);
    bool DeleteVehicle(int id); // ili da e void ?
    public bool MechanicToVehicle(Mechanic mechanic, Vehicle vehicle); // ili da e v imehanik repositorito
}