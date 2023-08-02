using AutoMapper;
using VianaSoft.Partner.App.AutoMapper;

namespace VianaSoft.Partner.Tests._Setup
{
    public static class AutoMapperHelperTest
    {
        public static IMapper GetInstance()
        {
            var mappingProfile = new MappingProfile();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(mappingProfile);
            });
            return new Mapper(config);
        }
    }
}
