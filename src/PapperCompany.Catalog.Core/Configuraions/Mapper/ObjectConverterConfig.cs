using AutoMapper;
using PapperCompany.Catalog.Core.Configurations.Mapper.Profiles;

namespace PapperCompany.Catalog.Core.Configurations.Mapper;

public class ObjectConverterConfig
{
    public MapperConfiguration RegisterMappings()
    {
        return new(
            configuration =>
            {
                configuration.AddProfile(new RequestToArgumentProfile());
                configuration.AddProfile(new ModelToResponseProfile());
            });
    }
}
