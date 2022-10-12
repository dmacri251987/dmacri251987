using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Models
{
    public class ProductDto
    {

        public ProductDto()
        {
            Count = 1;
        }
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
        [Range(1, 100)]
        public int Count { get; set; }
    }
}
