using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mango.MessageBus
{
    public class BaseMessages
    {
        public int Id { get; set; }
        public DateTime MessageCreated { get; set; }

    }
}
