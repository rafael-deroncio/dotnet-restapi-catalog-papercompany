using AutoMapper;
using PapperCompany.Catalog.Core.Models;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Core;

public class ModelToResponseProfile : Profile
{
    public ModelToResponseProfile()
    {
        CreateMap<CategoryModel, CategoryResponse>().ReverseMap();
        CreateMap<ProductModel, ProductResponse>().ReverseMap();
    }
}
