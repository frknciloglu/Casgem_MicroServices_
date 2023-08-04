using AutoMapper;
using Microservices.Catalog.Dtos.CategoryDtos;

using Microservices.Catalog.Dtos.ProductDto;

using Microservices.Catalog.Models;

namespace Microservices.Catalog.Mapping
{
    public class GeneralMapping:Profile
    {
        public GeneralMapping()
        {
            CreateMap<Category,ResultCategoryDto>().ReverseMap();

            CreateMap<Category,CreateCategoryDto>().ReverseMap();
            CreateMap<Category,UpdateCategoryDto>().ReverseMap();


            CreateMap<Products,ResultProductDto>().ReverseMap();
            CreateMap<Products,UpdateProductDto>().ReverseMap();
            CreateMap<Products,UpdateProductDto>().ReverseMap();


        }
    }
}
