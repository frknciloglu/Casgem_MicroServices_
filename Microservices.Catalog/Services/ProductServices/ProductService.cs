using AutoMapper;
using Microservices.Catalog.Dtos.CategoryDtos;
using Microservices.Catalog.Dtos.ProductDto;
using Microservices.Catalog.Models;
using Microservices.Catalog.Settings.Abstract;
using MicroServices.Shared.Dtos;
using MongoDB.Driver;

namespace Microservices.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IMongoCollection<Products> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        public ProductService(IMapper mapper ,IDataBaseSettings _databaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(_databaseSettings.ConnectionString);
            var database = client.GetDatabase(_databaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName);
            _productCollection = database.GetCollection<Products>(_databaseSettings.ProductCollectionName);
        }

        public async Task<Response<CreateProductDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            var values=_mapper.Map<Products>(createProductDto);
            await _productCollection.InsertOneAsync(values);
            return Response<CreateProductDto>.Success(_mapper.Map<CreateProductDto>(values), 200);
        }

        public async Task<Response<NoContent>> DeleteProductAsync(string id)
        {
            var value=await _productCollection.DeleteOneAsync(id);
            if (value.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("Silinecek ürün bulunamadı", 404);
            }
        }

        public async Task<Response<ResultProductDto>> GetProductByIdAsync(string id)
        {
            var values = await _productCollection.Find<Products>(x => x.ProductID == id).FirstOrDefaultAsync();
            if (values == null)
            {
                return Response<ResultProductDto>.Fail("Böyle bir Id Bulunamadı", 404);
            }
            else
            {
                return Response<ResultProductDto>.Success(_mapper.Map<ResultProductDto>(values), 200);
            }
        }

        public async Task<Response<List<ResultProductDto>>> GetProductListAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            return Response<List<ResultProductDto>>.Success(_mapper.Map<List<ResultProductDto>>(values),200);
        }

        public async Task<Response<List<ResultProductDto>>> GetProductListWithCategoryAsync()
        {
            var values = await _productCollection.Find(x => true).ToListAsync();
            if (values.Any())
            {
                foreach(var item in values)
                {
                    item.Category = await _categoryCollection.Find<Category>(x => x.CategoryID == item.CategoryID).FirstOrDefaultAsync();
                }
            }
            else
            {
                values = new List<Products>();
            }
            return Response<List<ResultProductDto>>.Success(_mapper.Map<List<ResultProductDto>>(values), 200);
        }

        public async Task<Response<UpdateProductDto>> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var value = _mapper.Map<Products>(updateProductDto);
            var result =await _productCollection.FindOneAndReplaceAsync(x=>x.ProductID==updateProductDto.ProductID, value);
            if (result == null)
            {
                return Response<UpdateProductDto>.Fail("Güncellenecek veri bulunamadı", 404);

            }
            else
            {
                return Response<UpdateProductDto>.Success(204);
            }
        }
    }
}
