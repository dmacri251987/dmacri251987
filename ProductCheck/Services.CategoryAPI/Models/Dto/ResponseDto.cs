using System.ComponentModel.DataAnnotations.Schema;

namespace Services.CategoryAPI.Models.Dto
{
    public class ResponseDto
    {

        public bool IsSuccess { get; set; }
        public object Result { get; set; }
        public string DisplayMessage { get; set; }
        public List<string> ErrorMessages { get; set; }

    }
}
