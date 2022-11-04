using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesProduct.Business.DTOs
{
    public class ResponsneDto
    {
        public bool IsSuccess { get; set; }
        public object Result { get; set; }
        public string DisplayMessage { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
