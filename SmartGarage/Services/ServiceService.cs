using AutoMapper;
using SmartGarage.Exceptions;
using SmartGarage.Models;
using SmartGarage.Models.DTOs;
using SmartGarage.Models.DTOs.ServiceDTO;
using SmartGarage.Models.QueryParameters;
using SmartGarage.Repositories;
using SmartGarage.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SmartGarage.Services
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ServiceService(IServiceRepository serviceRepository, IUserService userService, IMapper mapper)
        {
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public CreateServiceResponseDTO Create(ServiceRequestDTO newService, string username)
        {
            _userService.IsCurrentUserEmployee(username);

            if (_serviceRepository.ServiceExists(newService.Name))
                throw new DuplicateEntityExcetion("This service already exists");

            var service = _mapper.Map<Service>(newService);
            var createdService = _serviceRepository.Create(service);

            return _mapper.Map<CreateServiceResponseDTO>(createdService);
        }

        public IList<ServiceResponseDTO> GetAll()
        {
            var services = _serviceRepository.GetAll();
            return _mapper.Map<IList<ServiceResponseDTO>>(services);
        }

        public IList<ServiceResponseDTO> FilterBy(ServicesQueryParameters filterParameters)
        {
            var services = _serviceRepository.FilterBy(filterParameters);
            return _mapper.Map<IList<ServiceResponseDTO>>(services);
        }

        public ServiceResponseDTO GetById(int id, string username)
        {
            _userService.IsCurrentUserEmployee(username);
            var service = _serviceRepository.GetById(id);
            return _mapper.Map<ServiceResponseDTO>(service);
        }

        public ServiceResponseDTO GetByName(string name)
        {
            var service = _serviceRepository.GetByName(name);
            return _mapper.Map<ServiceResponseDTO>(service);
        }

        public UpdateServiceResponseDTO Update(int id, ServiceRequestDTO updatedService, string username)
        {
            _userService.IsCurrentUserEmployee(username);
            var serviceToUpdate = _mapper.Map<Service>(updatedService);
            var updatedServiceEntity = _serviceRepository.Update(id, serviceToUpdate);
            return _mapper.Map<UpdateServiceResponseDTO>(updatedServiceEntity);
        }

        public DeleteServiceResponseDTO Delete(int id, string username)
        {
            _userService.IsCurrentUserEmployee(username);
            var deletedService = _serviceRepository.Delete(id);
            return _mapper.Map<DeleteServiceResponseDTO>(deletedService);
        }

        public bool ServiceExists(string name)
        {
            return _serviceRepository.ServiceExists(name);
        }

        public int Count()
        {
            return _serviceRepository.Count();
        }
    }
}
