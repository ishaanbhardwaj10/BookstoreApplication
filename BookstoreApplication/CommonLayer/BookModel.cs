using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer
{
    public class BookModel
    {
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public float Ratings { get; set; }
        public int NoOfPeopleRated { get; set; }
        public int DiscountedPrice { get; set; }
        public int OriginalPrice { get; set; }
        public string BookDetails { get; set; }
        public string BookImage { get; set; }
        public int BookQuantity { get; set; }
    }
}
