using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace customOrder.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        public string StName { get; set; }

        public string Phone { get; set; }


    }
}
