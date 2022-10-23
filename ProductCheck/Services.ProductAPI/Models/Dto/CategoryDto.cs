namespace Services.ProductAPI.Models.Dto
{
    public class CategoryDto
    {

      
        public int CategoryID { get; set; }
      
        public string CategoryName { get; set; }

        public string Description { get; set; }

        public byte[] Picture { get; set; }

    }
}
