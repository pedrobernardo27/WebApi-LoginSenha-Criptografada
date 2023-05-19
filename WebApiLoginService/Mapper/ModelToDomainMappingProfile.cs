using AutoMapper;
using WebApiLoginRepository.Model;

namespace WebApiLoginService.Mapper
{
    public class ModelToDomainMappingProfile : Profile
    {
        public ModelToDomainMappingProfile()
        {
            CreateMap<PessoaRequest, Pessoa>();
        }
    }

    public class DomainToModelMappingProfile : Profile
    {
        public DomainToModelMappingProfile()
        {
            CreateMap<Pessoa, PessoaRequest>();            
        }
    }
}
