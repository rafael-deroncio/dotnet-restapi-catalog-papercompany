using AutoMapper;
using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Domain.Requests;

namespace PapperCompany.Catalog.Core.Configurations.Mapper.Profiles;

public class RequestToArgumentProfile : Profile
{
    public RequestToArgumentProfile()
    {
        // CreateMap<CategoryRequest, CategoryArgument>().ReverseMap();
        // CreateMap<ProductRequest, ProductArgument>().ReverseMap();
        CreateMap<PaginationRequest, PaginationArgument>().ReverseMap();
    }
}
