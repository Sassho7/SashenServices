using SmartGarage.Models;
using SmartGarage.Models.QueryParameters;
using System.Collections.Generic;

namespace SmartGarage.Repositories
{
    public interface IVehicleRepository
    {
        Vehicle Create(User user, Vehicle vehicle);
        IList<Vehicle> GetAll();
        Vehicle GetById(int id);
        IList<string> GetLicensePlateByUser(string username);
        Vehicle Update(int vehicleId, Vehicle updatedVehicle);
        IList<Vehicle> FilterBy(VehicleQueryParameters filterParameters);
        Vehicle FilterByLicensePlate(string licensePlate);
        List<Vehicle> SearchByPhoneNumber(string phoneNumber);
        IList<Vehicle> SearchBy(string filter);
        bool Delete(int id);
    }
}
