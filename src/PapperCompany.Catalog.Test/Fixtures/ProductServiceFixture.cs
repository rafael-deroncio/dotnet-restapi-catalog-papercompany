﻿using AutoFixture;
using Microsoft.Extensions.Logging;
using NSubstitute;
using PapperCompany.Catalog.Core.Arguments;
using PapperCompany.Catalog.Core.Configurations.Mapper.Interfaces;
using PapperCompany.Catalog.Core.Models;
using PapperCompany.Catalog.Core.Repositories.Interfaces;
using PapperCompany.Catalog.Core.Services;
using PapperCompany.Catalog.Core.Services.Interfaces;
using PapperCompany.Catalog.Domain.Requests;
using PapperCompany.Catalog.Domain.Responses;

namespace PapperCompany.Catalog.Test.Fistures;

public class ProductServiceFixture
{
    private readonly IFixture _fixture;
    private readonly ILogger<ProductService> _logger;
    private readonly IObjectConverter _mapper;
    private readonly ICategoryService _categoryService;
    private readonly IPaginationService _paginationService;
    private readonly IProductRepository _productRepository;

    public ProductServiceFixture()
    {
        _fixture = new Fixture();
        _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        _logger = Substitute.For<ILogger<ProductService>>();
        _mapper = Substitute.For<IObjectConverter>();
        _categoryService = Substitute.For<ICategoryService>();
        _paginationService = Substitute.For<IPaginationService>();
        _productRepository = Substitute.For<IProductRepository>();
    }

    public ProductService InstantiateService()
    {
        return new ProductService(
            _logger,
            _mapper,
            _categoryService,
            _paginationService,
            _productRepository
            );
    }

    #region Fixtures
    public ProductServiceFixture WithGetProduct(int? id = null, bool success = true)
    {
        id ??= Arg.Any<int>();
        ProductModel? model = success ? _fixture.Create<ProductModel>() : null;
        _productRepository.GetProduct(id.Value).Returns(model);
        return this;
    }

    public ProductServiceFixture WithGetProducts()
    {
        PaginationArgument argument = Arg.Any<PaginationArgument>();
        IEnumerable<ProductModel> models = _fixture.CreateMany<ProductModel>();
        _productRepository.GetProducts(argument).Returns(models);
        return this;
    }

    public ProductServiceFixture WithGetPagination()
    {
        PaginationRequest request = Arg.Any<PaginationRequest>();
        int total = Arg.Any<int>();
        IEnumerable<ProductResponse> content = Arg.Any<IEnumerable<ProductResponse>>();
        PaginationResponse<ProductResponse> response = _fixture.Create<PaginationResponse<ProductResponse>>();
        _paginationService.GetPagination(request, total, content).Returns(response);
        return this;
    }

    public ProductServiceFixture WithCreateProduct(int? id)
    {
        ProductArgument argument = Arg.Any<ProductArgument>();
        ProductModel model = _fixture.Create<ProductModel>();

        model.ProductId = id ??= id.Value;
        model.Category.CategoryId = id ??= id.Value;

        _productRepository.CreateProduct(argument).Returns(model);
        return this;
    }

    public ProductServiceFixture WithGetCategory(int? id, bool success)
    {
        id ??= Arg.Any<int>();
        CategoryResponse? response = null;

        if (success)
        {
            response = _fixture.Create<CategoryResponse>();
            response.CategoryId = id.Value;
        }

        _categoryService.GetCategory(id.Value).Returns(response);
        return this;
    }

    public ProductServiceFixture WithUpdateProduct()
    {
        ProductArgument argument = Arg.Any<ProductArgument>();
        ProductModel model = _fixture.Create<ProductModel>();
        _productRepository.UpdateProduct(argument).Returns(model);
        return this;
    }

    public ProductServiceFixture WithGetTotalRecords()
    {
        int total = _fixture.Create<int>();
        _productRepository.GetTotalRecords().Returns(total);
        return this;
    }

    public ProductServiceFixture WithDeleteProduct(bool result)
    {
        int id = Arg.Any<int>();
        _productRepository.DeleteProduct(id).Returns(result);
        return this;
    }
    #endregion

    #region Mappers
    public ProductServiceFixture WithMapModelToResponse()
    {
        ProductModel model = Arg.Any<ProductModel>();
        ProductResponse response = _fixture.Create<ProductResponse>();
        _mapper.Map<ProductResponse>(model).Returns(response);
        return this;
    }

    public ProductServiceFixture WithMapPaginationRequestToArgument()
    {
        PaginationRequest request = Arg.Any<PaginationRequest>();
        PaginationArgument argument = _fixture.Create<PaginationArgument>();
        _mapper.Map<PaginationArgument>(request).Returns(argument);
        return this;
    }

    public ProductServiceFixture WithMapProductRequestToArgument()
    {
        ProductRequest request = Arg.Any<ProductRequest>();
        ProductArgument argument = _fixture.Create<ProductArgument>();
        _mapper.Map<ProductArgument>(request).Returns(argument);
        return this;
    }

    public ProductServiceFixture WithMapArgumentToRequest()
    {
        PaginationArgument argument = Arg.Any<PaginationArgument>();
        PaginationRequest request = _fixture.Create<PaginationRequest>();
        _mapper.Map<PaginationRequest>(argument).Returns(request);
        return this;
    }

    public ProductServiceFixture WithMapModelToResponseList()
    {
        IEnumerable<ProductModel> models = Arg.Any<IEnumerable<ProductModel>>();
        IEnumerable<ProductResponse> responses = _fixture.CreateMany<ProductResponse>();
        _mapper.Map<IEnumerable<ProductResponse>>(models).Returns(responses);
        return this;
    }
    #endregion

    #region Mocks
    public PaginationRequest PaginationRequestMock()
    {
        return _fixture.Create<PaginationRequest>();
    }

    public ProductRequest ProductRequestMock(string name = "", string description = "", int? categoryId = null)
    {
        ProductRequest request = _fixture.Create<ProductRequest>();

        request.Name = string.IsNullOrEmpty(name) ? request.Name : name;
        request.Description = string.IsNullOrEmpty(description) ? request.Description : description;
        request.CategoryId = categoryId ?? request.CategoryId;

        return request;
    }
    #endregion
}
