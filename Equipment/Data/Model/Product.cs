using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Equipment.Data.Model
{
    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
