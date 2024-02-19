using SmartGarage.Exceptions;
using SmartGarage.Models;
using SmartGarage.Models.DTOs;
using SmartGarage.Services;
using Microsoft.AspNetCore.Mvc;
using SmartGarage.Models.DTOs.VehicleDTO;

namespace SmartGarage.Controllers.API;

[Route("api/vehicles")]
public class VehicleController : ControllerBase
{
    private readonly IVehicleService vehicleService;

    public VehicleController(IVehicleService vehicleService)
    {
        this.vehicleService = vehicleService;
    }

    [HttpGet]
    public IActionResult GetAllVehicles()
    {
        var vehicles = vehicleService.GetAllVehicles();

        return Ok(vehicles);
    }

    [HttpGet("{id}")]
    public IActionResult GetVehicle(int id)
    {
        var vehicle = vehicleService.GetVehicleById(id);

        if (vehicle == null)
        {
            return NotFound();
        }

        return Ok(vehicle);
    }

    [HttpPost]
    public IActionResult CreateVehicle([FromBody] VehicleCreateDTO dto)
    {
        try
        {
            var createVehicle = vehicleService.CreateVehicle(dto);

            return Ok(createVehicle);
        }
        catch (Exception e)
        {

            return StatusCode(StatusCodes.Status404NotFound, e.Message);
        }
    }

    [HttpPut("{id}")]
    public IActionResult UpdateVehicle(int id, [FromBody] VehicleUpdateDTO dto)
    {
        try
        {
            var updateVehicle = vehicleService.UpdateVehicle(id, dto);

            return Ok(updateVehicle);
        }
        catch (Exception e)
        {

            return StatusCode(StatusCodes.Status404NotFound, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteVehicle(int id)
    {
        try
        {
            var delete = vehicleService.DeleteVehicle(id);

            return Ok(delete);
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status404NotFound, e.Message);
        }
    }
}