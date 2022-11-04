namespace ApiGateway.Models
{
    public class ProductDto
    {
        public ProductDto(List<CategoryDto> categories)
        {
            Categories = new List<CategoryDto>();
        }

        public int ProductID { get; set; }

        public string ProductName { get; set; }
        public int? SupplierID { get; set; }
        public int? CategoryID { get; set; }
        public string QuantityPerUnit { get; set; }

        public decimal? UnitPrice { get; set; }
        public short? UnitsInStock { get; set; }
        public short? UnitsOnOrder { get; set; }
        public short? ReorderLevel { get; set; }
        public bool Discontinued { get; set; }

        public List<CategoryDto> Categories { get; set; }
    }
}
