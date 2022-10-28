using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.DTOs.Product
{
    public class ProductDto : ResponseDto
    {
        public int ProductID { get; set; }

        [Display(Name = "Nombre")]
        public string ProductName { get; set; }
        [Display(Name = "Proveedor")]
        public int? SupplierID { get; set; }
        [Display(Name = "Categoria")]
        public int? CategoryID { get; set; }
        [Display(Name = "Cantidad Percio Unidad")]
        public string QuantityPerUnit { get; set; }
        [Display(Name = "Percio Unidad")]
        public decimal? UnitPrice { get; set; }
        [Display(Name = "En Stock")]
        public short? UnitsInStock { get; set; }
        [Display(Name = "Cantidad")]
        public short? UnitsOnOrder { get; set; }
        [Display(Name = "ReorderLevel")]
        public short? ReorderLevel { get; set; }
        [Display(Name = "Discontinuado")]
        public bool Discontinued { get; set; }
    }
}
