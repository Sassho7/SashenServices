using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SmartGarage.Models.QueryParameters;
using SmartGarage.Services;
using SmartGarage.ViewModels;

namespace SmartGarage.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly IVehicleService _vehicleService;
        private readonly IMapper autoMapper;
        public VehiclesController(IVehicleService vehicleService, IMapper autoMapper)
        {
            _vehicleService = vehicleService;
            this.autoMapper = autoMapper;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var vehicleResponseDTO = _vehicleService.GetAll();

            var vehicleViewModels = autoMapper.Map<IList<VehicleViewModel>>(vehicleResponseDTO);

            return View(vehicleViewModels);
        }
        public IActionResult Details(int id)
        {
            var vehicle = _vehicleService.GetById(id);
            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }
    }
}
