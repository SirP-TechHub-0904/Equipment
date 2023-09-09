using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Equipment.Data.Model
{
    public class Contact
    {
        public long Id { get; set; }
        public string Message { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
