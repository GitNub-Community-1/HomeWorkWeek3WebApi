using AutoMapper;
using MiniLibraryAPI.Entities;
using MiniLibraryAPI.Models.DTOs;

namespace MiniLibraryAPI.AutoMapper;
using AutoMapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<CreateOrderDto,Order>().ReverseMap();
        CreateMap<OrderDto, Order>().ReverseMap();
        CreateMap<CreateAuthorDto,Author>().ReverseMap();
        CreateMap<AuthorDto,Author>().ReverseMap();
        CreateMap<CreateBookDto,Book>().ReverseMap();
        CreateMap<BookDto,Book>().ReverseMap();
        CreateMap<CreateCategoryDto,Category>().ReverseMap();
        CreateMap<CategoryDtos,Category>().ReverseMap();
    }
}