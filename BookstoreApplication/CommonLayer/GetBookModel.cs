using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer
{
    public class GetBookModel
    {
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public decimal Ratings { get; set; }
        public int NoOfPeopleRated { get; set; }
        public int DiscountedPrice { get; set; }
        public int OriginalPrice { get; set; }
        public string BookDetails { get; set; }
        public string BookImage { get; set; }
        public int BookQuantity { get; set; }
    }
}
