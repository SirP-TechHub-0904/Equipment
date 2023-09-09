using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Equipment.Data.Model
{
    public class Image
    {
        public long Id { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime Date { get; set; }
    }
}
