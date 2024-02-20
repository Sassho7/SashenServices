using AutoMapper;
using SmartGarage.Models;
using SmartGarage.Models.DTOs;
using SmartGarage.Models.DTOs.ServiceDTO;
using SmartGarage.Models.DTOs.UserDTO;
using SmartGarage.Models.DTOs.VehicleDTO;
using SmartGarage.ViewModels;

namespace SmartGarage.Helpers
{
    public class MapperProfiles : Profile
    {
        public MapperProfiles()
        {

            this.CreateMap<User, UserResponseDTO>();

            this.CreateMap<UserRequestDTO, User>();


  
            this.CreateMap<UpdateUserRequestDTO, User>();

            this.CreateMap<UserResponseDTO, UserRequestDTO>();
   

         
            this.CreateMap<RegisterViewModel,UserRegistrationDTO>();

  


         

            this.CreateMap<Vehicle, VehicleResponseDTO>()
                .ForPath(dest => dest.User.Id, opt => opt.MapFrom(src => src.User.Id))
                .ForPath(dest => dest.User.Username, opt => opt.MapFrom(src => src.User.Username))
                .ForPath(dest => dest.User.Email, opt => opt.MapFrom(src => src.User.Email))
                .ForPath(dest => dest.User.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber));
        

            this.CreateMap<ServiceResponseDTO, ServiceViewModel>();

        

            this.CreateMap<VehicleResponseDTO, VehicleViewModel>();
   


     

            this.CreateMap<VehicleRequestDTO, Vehicle>();
            this.CreateMap<VehicleRequestDTO, CreateVehicleViewModel>();

    

            this.CreateMap<VehicleResponseDTO, Vehicle>();
            this.CreateMap<VehicleResponseDTO, VehicleViewModel>();

      

            this.CreateMap<Service, ServiceResponseDTO>();


      

            this.CreateMap<Service, UpdateServiceResponseDTO>();
            this.CreateMap<Service, CreateServiceResponseDTO>();
            this.CreateMap<Service, DeleteServiceResponseDTO>();
            this.CreateMap<Service, ServiceResponseDTO>();

        

            this.CreateMap<ServiceResponseDTO, ServiceViewModel>();


         

            this.CreateMap<ServiceViewModel, ServiceRequestDTO>();
            this.CreateMap<ServiceViewModel, ServiceRequestDTO>();

      

            this.CreateMap<VehicleViewModel, VehicleResponseDTO>();


        }
    }
}
