using AutoMapper;
using rei_esperantolib.Models;

namespace rei_identityserver.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<RegistroUsuarioModel, Usuario>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Nome))
            .ForMember(u => u.PasswordHash, opt => opt.MapFrom(x => x.Senha));

        CreateMap<AlteraUsuarioModel, Usuario>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Nome))
            .ForMember(u => u.Email, opt => opt.MapFrom(x => x.Email))
            .ForMember(u => u.PhoneNumber, opt => opt.MapFrom(x => x.Telefone));
    }
}
