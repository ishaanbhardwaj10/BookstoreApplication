using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer
{
    public class GetCartModel
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int CartQuantity { get; set; }
    }
}
