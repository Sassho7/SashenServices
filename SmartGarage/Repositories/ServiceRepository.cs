using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SmartGarage.Data;
using SmartGarage.Exceptions;
using SmartGarage.Models;
using SmartGarage.Models.QueryParameters;
using SmartGarage.Repositories;

namespace SmartGarage.Repositories
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly SGDbContext _context;

        public ServiceRepository(SGDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Service Create(Service newService)
        {
            if (newService == null)
            {
                throw new ArgumentNullException(nameof(newService));
            }
            try
            {
                _context.Services.Add(newService);
                _context.SaveChanges();
                return newService;
            }
            catch (DbUpdateException ex)
            {
                throw new RepositoryException("Failed to create service. See inner exception for details.", ex);
            }
        }

        public IList<Service> GetAll()
        {
            return _context.Services.Where(s => !s.isDeleted).ToList();
        }

        public IList<Service> FilterBy(ServicesQueryParameters serviceParams)
        {
            IQueryable<Service> result = _context.Services.Where(u => !u.isDeleted);

            if (!string.IsNullOrEmpty(serviceParams.Name))
            {
                result = result.Where(s => s.Name == serviceParams.Name);
            }
            if (serviceParams.MaxPrice.HasValue)
            {
                result = result.Where(s => s.Price <= serviceParams.MaxPrice);
            }
            if (serviceParams.MinPrice.HasValue)
            {
                result = result.Where(s => s.Price >= serviceParams.MinPrice);
            }

            switch (serviceParams.SortBy)
            {
                case "name":
                    result = result.OrderBy(s => s.Name);
                    break;
                case "price":
                    result = result.OrderBy(s => s.Price);
                    break;
                default:
                    break;
            }

            result = (serviceParams.SortOrder == "desc") ? result.Reverse() : result;
            return result.ToList();
        }

        public Service GetById(int id)
        {
            return _context.Services.FirstOrDefault(s => s.ServiceId == id && !s.isDeleted) ??
                   throw new EntityNotFoundException($"Service with id:{id} not found.");
        }

        public Service GetByName(string name)
        {
            return _context.Services.FirstOrDefault(s => s.Name == name && !s.isDeleted) ??
                   throw new EntityNotFoundException($"Service with name:{name} is not found.");
        }

        public Service Update(int id, Service updatedService)
        {
            Service existingService = GetById(id);

            existingService.Name = updatedService.Name;
            existingService.Price = updatedService.Price;

            try
            {
                _context.SaveChanges();
                return updatedService;
            }
            catch (DbUpdateException ex)
            {

                throw new RepositoryException("Service Update Failure", ex);
            }
        }

        public Service Delete(int id)
        {
            Service toDelete = GetById(id);
            toDelete.isDeleted = true;

            try
            {
                _context.SaveChanges();
                return toDelete;
            }
            catch (DbUpdateException ex)
            {

                throw new RepositoryException("Service Deletion Failure.", ex);
            }
        }
        public int Count()
        {
            return _context.Services.Count();
        }
        public bool ServiceExists(string name)
        {
            return _context.Services.Any(s => s.Name == name);
        }

       
    }
}
