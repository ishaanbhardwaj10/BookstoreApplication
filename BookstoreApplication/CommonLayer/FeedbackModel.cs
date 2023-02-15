using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CommonLayer
{
    public class FeedbackModel
    {
        public int Ratings { get; set; }
        public string Comment { get; set; }
        public int BookId { get; set; }

    }
}
