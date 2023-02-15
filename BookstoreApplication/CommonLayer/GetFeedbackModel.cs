using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer
{
    public class GetFeedbackModel
    {
        public int FeedbackId { get; set; }
        public int Ratings { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
