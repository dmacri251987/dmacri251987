using System.ComponentModel.DataAnnotations;

namespace Services.CategoryAPI.Models
{
    public class Category
    {

        [Key]
        public int CategoryID { get; set; }
        [Required]
        [StringLength(15)]
        public string CategoryName { get; set; }
     
        public string Description { get; set; }

        [Required(AllowEmptyStrings =true)]
        public byte[] Picture { get; set; }


    }
}
