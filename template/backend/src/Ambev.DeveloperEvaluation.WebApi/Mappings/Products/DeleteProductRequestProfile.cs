﻿using Ambev.DeveloperEvaluation.Application.Product.DeleteProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Products;

public class DeleteProductRequestProfile : Profile
{
    public DeleteProductRequestProfile()
    {
        CreateMap<DeleteProductRequest, DeleteProductCommand>();
    }
}