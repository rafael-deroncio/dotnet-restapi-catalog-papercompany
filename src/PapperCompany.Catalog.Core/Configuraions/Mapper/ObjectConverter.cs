using AutoMapper;
using PapperCompany.Catalog.Core.Configurations.Mapper.Interfaces;

namespace PapperCompany.Catalog.Core.Configurations.Mapper;

public class ObjectConverter : IObjectConverter
{
    private readonly IMapper _mapper;

    public ObjectConverter()
    {
        _mapper = new ObjectConverterConfig().RegisterMappings().CreateMapper();
    }

    public T Map<T>(object source)
    {
        return _mapper.Map<T>(source);
    }

    public D Map<T, D>(T source, D destination)
    {
        return source is null ? destination : _mapper.Map<T, D>(source, destination);
    }
}
