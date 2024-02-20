using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartGarage.Services;
using System.Security.Claims;
using SmartGarage.Helpers;
using AutoMapper;
using SmartGarage.Exceptions;
using SmartGarage.Models.QueryParameters;
using SmartGarage.Models.DTOs.UserDTO;
using SmartGarage.Models.DTOs.VehicleDTO;

namespace SmartGarage.Controllers.API
{
    [ApiController]
    [Route("api/vehicles")]
    [Authorize]
    public class VehiclesApiController : ControllerBase
    {
        private readonly IVehicleService vehicleService;
        private readonly IUserService usersService;
        private readonly IMapper autoMapper;
        public VehiclesApiController(IVehicleService vehicleService, IUserService usersService, IModelMapper modelMapper, IMapper autoMapper)
        {
            this.vehicleService = vehicleService;
            this.usersService = usersService;
            this.autoMapper = autoMapper;
        }

        [HttpPost]
        public IActionResult CreateVehicle([FromBody] VehicleRequestDTO dto)
        {
            try
            {
                var user = GetUser();

                var createdVehicle = vehicleService.Create(user.Username, dto);

                return StatusCode(StatusCodes.Status201Created, createdVehicle);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult GetAllVehicles([FromQuery] VehicleQueryParameters filter)
        {
            try
            {
                var user = GetUser();

                var vehicles = this.vehicleService.FilterBy(filter);

                return Ok(vehicles);
            }
            catch (AuthorizationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetVehicle(int id)
        {
            try
            {
                var vehicle = vehicleService.GetById(id);

                return Ok(vehicle);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVehicle(int id, [FromBody] VehicleRequestDTO dto)
        {
            try
            {
                var user = GetUser();

                var updatedVehicle = vehicleService.Update(id, dto);

                return Ok(updatedVehicle);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }



        [HttpDelete("{id}")]
        public IActionResult DeleteVehicle(int id)
        {
            try
            {
                var user = GetUser();

                vehicleService.Delete(id);

                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (AuthorizationException ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        private UserRequestDTO GetUser()
        {
            var user = usersService.GetByName(User.FindFirst(ClaimTypes.Name)?.Value);
            if (!user.IsEmployee)
            {
                throw new AuthorizationException("Employees only");
            }
            return autoMapper.Map<UserRequestDTO>(user);
        }
    }
}
