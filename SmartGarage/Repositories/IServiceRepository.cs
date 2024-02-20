using System.Collections.Generic;
using SmartGarage.Models;
using SmartGarage.Models.QueryParameters;

namespace SmartGarage.Repositories
{
    public interface IServiceRepository
    {
        Service Create(Service newService);
        IList<Service> GetAll();
        IList<Service> FilterBy(ServicesQueryParameters serviceParams);
        Service GetById(int id);
        Service GetByName(string name);
        Service Update(int id, Service updatedService);
        Service Delete(int id);
        bool ServiceExists(string name);
        int Count();
    }
}
