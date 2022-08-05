using ApiChida.Models;
using AutoMapper;

namespace ApiChida.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<InputUsertModel, ApplicationUser>()
                .ForMember(destinationMember => destinationMember.UserName,
                opt => opt.MapFrom(src => $"{src.UserName}"))
                 .ForMember(destinationMember => destinationMember.FirstName,
                opt => opt.MapFrom(src => $"{src.FirstName}"))
                  .ForMember(destinationMember => destinationMember.LastName,
                opt => opt.MapFrom(src => $"{src.LastName}"))
                .ForMember(destinationMember => destinationMember.PhoneNumber,
                opt => opt.MapFrom(src => $"{src.PhoneNumber}"))
                .ForMember(destinationMember => destinationMember.PasswordHash,
                opt => opt.MapFrom(src => $"{src.PasswordHash}"))
                .ForMember(destinationMember => destinationMember.Email,
                opt => opt.MapFrom(src => $"{src.Email}"))
                .ForMember(destinationMember => destinationMember.IsEnabled,
                opt => opt.MapFrom(src => $"{src.IsEnabled}"));

            CreateMap<OutputUserModel, ApplicationUser>()
                .ForMember(destinationMember => destinationMember.Id,
                opt => opt.MapFrom(src => $"{src.Id}"))
                .ForMember(destinationMember => destinationMember.UserName,
                opt => opt.MapFrom(src => $"{src.UserName}"))
                 .ForMember(destinationMember => destinationMember.FirstName,
                opt => opt.MapFrom(src => $"{src.FirstName}"))
                  .ForMember(destinationMember => destinationMember.LastName,
                opt => opt.MapFrom(src => $"{src.LastName}"))
                .ForMember(destinationMember => destinationMember.PhoneNumber,
                opt => opt.MapFrom(src => $"{src.PhoneNumber}"))
                .ForMember(destinationMember => destinationMember.PasswordHash,
                opt => opt.MapFrom(src => $"{src.PasswordHash}"))
                .ForMember(destinationMember => destinationMember.Email,
                opt => opt.MapFrom(src => $"{src.Email}"))
                .ForMember(destinationMember => destinationMember.IsEnabled,
                opt => opt.MapFrom(src => $"{src.IsEnabled}"));

            CreateMap<ApplicationUser, ApplicationUser>()
                .ForMember(destinationMember => destinationMember.Id,
                opt => opt.MapFrom(src => $"{src.Id}"))
                .ForMember(destinationMember => destinationMember.UserName,
                opt => opt.MapFrom(src => $"{src.UserName}"))
                 .ForMember(destinationMember => destinationMember.FirstName,
                opt => opt.MapFrom(src => $"{src.FirstName}"))
                  .ForMember(destinationMember => destinationMember.LastName,
                opt => opt.MapFrom(src => $"{src.LastName}"))
                .ForMember(destinationMember => destinationMember.PhoneNumber,
                opt => opt.MapFrom(src => $"{src.PhoneNumber}"))
                .ForMember(destinationMember => destinationMember.PasswordHash,
                opt => opt.MapFrom(src => $"{src.PasswordHash}"))
                .ForMember(destinationMember => destinationMember.Email,
                opt => opt.MapFrom(src => $"{src.Email}"))
                .ForMember(destinationMember => destinationMember.IsEnabled,
                opt => opt.MapFrom(src => $"{src.IsEnabled}"));
        
    }


    }
}
