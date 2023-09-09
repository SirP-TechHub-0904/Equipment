using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Equipment.Data.Model
{
    public class Category
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
