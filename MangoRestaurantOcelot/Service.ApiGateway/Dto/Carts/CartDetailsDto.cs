using Service.ApiGateway.Dto.Products;

namespace Services.ShoppingCartAPI.Models.Dto;

public class CartDetailsDto
{

    public CartDetailsDto(List<ProductDto> product)
    {
        Product = new List<ProductDto>();
    }


    public int CartDetailsId { get; set; }
    public int CartHeaderId { get; set; }
    public virtual CartDto CartHeader { get; set; }
    public int ProductId { get; set; }
    public List<ProductDto> Product { get; set; }
    public int Count { get; set; }
}

