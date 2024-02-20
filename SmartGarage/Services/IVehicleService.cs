using SmartGarage.Models;
using SmartGarage.Models.DTOs.VehicleDTO;
using SmartGarage.Models.QueryParameters;
using System.Collections.Generic;

namespace SmartGarage.Services
{
    public interface IVehicleService
    {
        VehicleResponseDTO Create(string username, VehicleRequestDTO dto);
        IList<VehicleResponseDTO> GetAll();
        VehicleResponseDTO GetById(int id);
        IList<string> GetLicensePlateByUser(string username);
        VehicleResponseDTO Update(int vehicleId, VehicleRequestDTO dto);
        bool Delete(int id);
        IList<VehicleResponseDTO> FilterBy(VehicleQueryParameters vehicleQueryParameters);
        VehicleResponseDTO FilterByLicensePlate(string licensePlate);
        IList<Vehicle> SearchBy(string filter);
        List<Vehicle> SearchByPhoneNumber(User user, string phoneNumber);
    }
}
