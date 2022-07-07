using System;
using System.Linq;
using System.Threading.Tasks;

namespace PointOfSalesAPI.Models
{
    public class Customer
    {

        public int ID { get; set; }

        public string Name { get; set; }


        public string Phone { get; set; }


        public string Email { get; set; }

        public bool IsDeleted { get; set; }


    }
}
