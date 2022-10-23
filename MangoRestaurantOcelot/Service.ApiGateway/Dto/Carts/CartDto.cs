namespace Services.ShoppingCartAPI.Models.Dto;

public class CartDto
{
    public CartDto CartHeader { get; set; }
    public IEnumerable<CartDetailsDto> CartDetails { get; set; }
}
