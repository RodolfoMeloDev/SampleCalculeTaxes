using AutoMapper;
using CalculateTaxes.CrossCutting.Mappings;

namespace CalculateTaxes.Services.Test.Services
{
    public class ServiceBaseTest
    {
        protected readonly IMapper _mapper;

        public ServiceBaseTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToEntityProfile());
                cfg.AddProfile(new EntityToDtoProfile());
            });
            _mapper = config.CreateMapper();
        }
    }
}