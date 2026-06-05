using AutoMapper;
using backend.Helper;
using backend.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserInsertDto, User>()
            .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => HelperFunction.StringToSlug(src.Name)))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow));
        // CreateMap<User, UserInsertDto>(); // Mapping từ User sang UserDto

        // Mapping từ UserUpdateDto sang User
        CreateMap<UserUpdateDto, User>()
            .ForMember(dest => dest.Alias, opt => opt.MapFrom(src => HelperFunction.StringToSlug(src.Name)))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow)); 
    }
}