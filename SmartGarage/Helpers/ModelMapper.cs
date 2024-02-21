using Microsoft.Extensions.Hosting;
using SmartGarage.Helpers;
using SmartGarage.Models;
using SmartGarage.Models.DTOs;
using SmartGarage.Models.DTOs.VehicleDTO;

namespace SmartGarage.Helpers
{
    public interface IModelMapper
    {
        User Map(UserRegistrationDTO dto);
        Vehicle Map(VehicleRequestDTO dto, User user);
    }

    public class ModelMapper : IModelMapper
    {
      
        public User Map(UserRegistrationDTO dto)
        {
            return new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber
            };
        }

        public Vehicle Map(VehicleRequestDTO dto, User user)
        {
           return new Vehicle
          {
               CarLicencePlate = dto.LicensePlate,
               CarVin = dto.VIN,
               CarYear = dto.Year,
               CarModel = dto.Model,
               CarMake = dto.Brand
           };
        }

    }
}