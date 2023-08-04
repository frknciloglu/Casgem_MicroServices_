using MongoDB.Bson.Serialization.Attributes;

namespace Microservices.Catalog.Dtos.ProductDto
{
    public class CreateProductDto
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public string CategoryID { get; set; }
    }
}
