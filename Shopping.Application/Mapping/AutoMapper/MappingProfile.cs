using AutoMapper;
using Shopping.Domain.Dtos;
using Shopping.Domain.Entities;

namespace Shopping.Application.Mapping.AutoMapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<Domain.Entities.Product, ProductDto>().ReverseMap();
        CreateMap<ShoppingList, ShoppingListDto>().ReverseMap();
    }
}