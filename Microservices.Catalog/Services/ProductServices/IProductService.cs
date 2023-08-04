using Microservices.Catalog.Dtos.CategoryDtos;
using Microservices.Catalog.Dtos.ProductDto;
using MicroServices.Shared.Dtos;

namespace Microservices.Catalog.Services.ProductServices
{
    public interface IProductService
    {
        Task<Response<List<ResultProductDto>>> GetProductListAsync();
        Task<Response<ResultProductDto>> GetProductByIdAsync(string id);
        Task<Response<CreateProductDto>> CreateProductAsync(CreateProductDto createProductDto);
        Task<Response<UpdateProductDto>> UpdateProductAsync(UpdateProductDto updateProductDto);
        Task<Response<NoContent>> DeleteProductAsync(string id);
<<<<<<< HEAD
        Task<Response<List<ResultProductDto>>> GetProductListWithCategoryAsync();
=======
>>>>>>> 5b0bf79e2feec5810c3e9c3240edd8f0ea3ce443
    }
}
