using SmartGarage.Models.DTOs;
using SmartGarage.Models.DTOs.ServiceDTO;
using SmartGarage.Models.QueryParameters;
using System.Collections.Generic;

namespace SmartGarage.Services
{
    public interface IServiceService
    {
        CreateServiceResponseDTO Create(ServiceRequestDTO newService, string username);
        IList<ServiceResponseDTO> GetAll();
        IList<ServiceResponseDTO> FilterBy(ServicesQueryParameters filterParameters);
        ServiceResponseDTO GetById(int id, string username);
        ServiceResponseDTO GetByName(string name);
        UpdateServiceResponseDTO Update(int id, ServiceRequestDTO updatedService, string username);
        DeleteServiceResponseDTO Delete(int id, string username);
        bool ServiceExists(string name);
        int Count();
    }
}
