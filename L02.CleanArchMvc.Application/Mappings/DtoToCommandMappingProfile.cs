using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Mediator.CQRS.Products.Commands;

namespace CleanArchMvc.Application.Mappings;

public class DtoToCommandMappingProfile : Profile
{
    public DtoToCommandMappingProfile()
    {
        CreateMap<ProductDto, ProductCreateCommand>();
        CreateMap<ProductDto, ProductUpdateCommand>();
    }
}