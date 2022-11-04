using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Models.DTOs
{
    public class ResponseDto
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        [NotMapped]
        public string DisplayMessage { get; set; } = "";

        [NotMapped]
        public List<string> ErrorMessages { get; set; }
    }
}
