using SmartGarage.Models;
using SmartGarage.Models.QueryParameters;
using SmartGarage.Exceptions;
using AutoMapper;
using SmartGarage.ViewModels;
using SmartGarage.Models.DTOs.VehicleDTO;
using SmartGarage.Repositories;
using SmartGarage.Services;

namespace SmartGarage.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper autoMapper;

        public VehicleService(IVehicleRepository vehicleRepository, IMapper autoMapper, IUserRepository userRepository)
        {
            this.vehicleRepository = vehicleRepository;
            this.autoMapper = autoMapper;
            this.userRepository = userRepository;
        }

        public VehicleResponseDTO Create(string username, VehicleRequestDTO dto)
        {
            User user = userRepository.GetByUsername(username);
            if (!user.IsEmployee)
            {
                throw new AuthorizationException("Employees only!");
            }

            Vehicle vehicle = new Vehicle()
            {
                CarLicencePlate = dto.LicensePlate,
                CarVin = dto.VIN,
                CarYear= dto.Year,
                CarModel = dto.Model
            };

            Vehicle createdVehicle = this.vehicleRepository.Create(user, vehicle);
            VehicleResponseDTO dtoToReturn = new VehicleResponseDTO()
            {
                LicensePlate = createdVehicle.CarLicencePlate,
                VIN = createdVehicle.CarVin,
                Year = createdVehicle.CarYear,
                Model = autoMapper.Map<string>(createdVehicle.CarModel)
            };

            return dtoToReturn;
        }
        public IList<VehicleResponseDTO> GetAll()
        {
            IList<Vehicle> vehicles = this.vehicleRepository.GetAll();
            return vehicles
                .Select(v => autoMapper.Map<VehicleResponseDTO>(v))
                .ToList();
        }

        public VehicleResponseDTO GetById(int id)
        {

            Vehicle vehicle = this.vehicleRepository.GetById(id);
            return autoMapper.Map<VehicleResponseDTO>(vehicle);
        }

        public IList<string> GetLicensePlateByUser(string username)
        {
            return vehicleRepository.GetLicensePlateByUser(username);
        }

        public VehicleResponseDTO Update(int vehicleId, VehicleRequestDTO dto)
        {
            Vehicle updatedVehicle = this.vehicleRepository.Update(vehicleId, autoMapper.Map<Vehicle>(dto));
            return autoMapper.Map<VehicleResponseDTO>(updatedVehicle);

        }

        public bool Delete(int id)
        {
            return this.vehicleRepository.Delete(id);
        }

        public IList<VehicleResponseDTO> FilterBy(VehicleQueryParameters vehicleQueryParameters)
        {
            return this.vehicleRepository.FilterBy(vehicleQueryParameters)
                .Select(v => autoMapper.Map<VehicleResponseDTO>(v))
                .ToList();
        }

        public VehicleResponseDTO FilterByLicensePlate(string licensePlate)
        {
            var vehicleResponseDTO = autoMapper.Map<VehicleResponseDTO>(vehicleRepository.FilterByLicensePlate(licensePlate));
            return vehicleResponseDTO;
        }


        public IList<Vehicle> SearchBy(string filter)
        {
            return this.vehicleRepository.SearchBy(filter);
        }

        public List<Vehicle> SearchByPhoneNumber(User user, string phoneNumber)
        {
            if (!user.IsEmployee)
            {
                throw new AuthorizationException("Employees only!");
            }
            return this.vehicleRepository.SearchByPhoneNumber(phoneNumber);
        }

    }
}
