using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SmartGarage.Models;
using SmartGarage.Models.DTOs;
using SmartGarage.Data;
using SmartGarage.Exceptions;
using SmartGarage.Models.QueryParameters;
using SmartGarage.Repositories;
using System;

namespace SmartGarage.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly SGDbContext context;
        public VehicleRepository(SGDbContext context)
        {
            this.context = context;
        }

        public Vehicle Create(User user, Vehicle vehicle)
        {
            if (context.Vehicles.Any(v => v.CarLicencePlate == vehicle.CarLicencePlate && !v.IsDeleted))
            {
                throw new DuplicateEntityExcetion($"Vehicle with this license plate already exists");
            }

            if (context.Vehicles.Any(v => v.CarVin == vehicle.CarVin && !v.IsDeleted))
            {
                throw new DuplicateEntityExcetion($"Vehicle with this VIN already exists");
            }

            vehicle.userId = user.Id;
            context.Vehicles.Add(vehicle);
            vehicle.User.Vehicles.Add(vehicle);
            context.SaveChanges();
            return vehicle;
        }
        public IList<Vehicle> GetAll()
        {
            return context.Vehicles
                .Where(v => !v.IsDeleted)
                .Include(v => v.User)
                .Include(v => v.CarMake)
                 .Include(v => v.CarModel)
                .ToList();
        }

        public Vehicle GetById(int id)
        {
            return context.Vehicles.FirstOrDefault(v => v.Id == id && !v.IsDeleted) ??
                throw new EntityNotFoundException($"Vehicle with id: {id} doesn't exists.");
        }

        public IList<string> GetLicensePlateByUser(string username)
        {
            return context.Vehicles
                .Where(v => !v.IsDeleted && v.User.Username == username)
                .Select(v => v.CarLicencePlate)
                .ToList();
        }

        public Vehicle Update(int vehicleId, Vehicle updatedVehicle)
        {
            Vehicle vehicleToUpdate = GetById(vehicleId);

            vehicleToUpdate.CarLicencePlate = updatedVehicle.CarLicencePlate;

            context.Update(vehicleToUpdate);
            context.SaveChanges();

            return vehicleToUpdate;
        }

        private IList<Vehicle> FilterByBrand(IList<Vehicle> vehicles, string brand)
        {
            if (!string.IsNullOrEmpty(brand))
            {
                return vehicles.Where(v => v.CarMake.Contains(brand)).ToList();
            }

            return vehicles;
        }


        public bool Delete(int id)
        {
            Vehicle vehicle = GetById(id);
            vehicle.IsDeleted = true;

            return context.SaveChanges() > 0;
        }
        private IList<Vehicle> FilterByModel(IList<Vehicle> vehicles, string model)
        {
            if (!string.IsNullOrEmpty(model))
            {
                return vehicles.Where(v => v.CarModel.Contains(model)).ToList();
            }

            return vehicles;
        }

        public IList<Vehicle> FilterBy(VehicleQueryParameters filterParameters)
        {
            IList<Vehicle> vehicles = GetAll();

            vehicles = FilterByModel(vehicles, filterParameters.Model);
            vehicles = FilterByBrand(vehicles, filterParameters.Brand);
            vehicles = FilterByYearOfCreation(vehicles, filterParameters.YearOfCreation);
            vehicles = SortBy(vehicles, filterParameters.SortBy);

            return vehicles.ToList();
        }
        public Vehicle FilterByLicensePlate(string licensePlate)
        {
            var vehicles = context.Vehicles
                .Where(v => !v.IsDeleted)
                .Include(v => v.User)
                .Include(v => v.CarModel)
                .ToList();

            Vehicle vehicle;
            if (!string.IsNullOrEmpty(licensePlate))
            {
                if (vehicles.FirstOrDefault(v => v.CarLicencePlate == licensePlate) != null)
                {
                    return vehicles.FirstOrDefault(v => v.CarLicencePlate == licensePlate);
                }
                else
                {
                    throw new EntityNotFoundException($"Vehicle with this doesn't exists.");
                }

            }else
                throw new EntityNotFoundException($"Please enter a valid license plate");
        }

       
        public List<Vehicle> SearchByPhoneNumber(string phoneNumber)
        {
            return GetAll().Where(v => v.User.PhoneNumber == phoneNumber).ToList() ??
            throw new EntityNotFoundException($"User with phone number: {phoneNumber} doesn't exists");
        }
        public IList<Vehicle> SearchBy(string filter)
        {
            var vehicles = context.Vehicles
            .Where(v => v.CarLicencePlate.Contains(filter) ||
                        v.CarVin.Contains(filter))
            .Include(v => v.CarMake)
            .Include(v => v.CarModel)
            .ToList();

            return vehicles;
        }

        

        private IList<Vehicle> FilterByYearOfCreation(IList<Vehicle> vehicles, string year)
        {
            if (!string.IsNullOrEmpty(year))
            {
                int _year = int.Parse(year);
                return vehicles.Where(v => v.CarYear.Equals(_year)).ToList();
            }

            return vehicles;
        }

        private IList<Vehicle> SortBy(IList<Vehicle> vehicles, string criteria)
        {
            switch (criteria)
            {
                case "model":
                    return vehicles.OrderBy(v => v.CarModel).ToList();
                case "brand":
                    return vehicles.OrderBy(v => v.CarMake).ToList();
                case "year":
                    return vehicles.OrderBy(v => v.CarYear).ToList();
                default:
                    return vehicles;
            }
        }
    }
}
