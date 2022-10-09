using System.ComponentModel;

namespace Mango.Web.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        [DisplayName("Nombre")]
        public string Name { get; set; }
        [DisplayName("Precio")]
        public double Price { get; set; }
        [DisplayName("Descripción")]
        public string Description { get; set; }
        [DisplayName("Categoria")]
        public string CategoryName { get; set; }
        [DisplayName("Imagen")]
        public string ImageUrl { get; set; }
    }
}
