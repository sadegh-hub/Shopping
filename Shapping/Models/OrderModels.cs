using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Shapping.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string address { get; set; }
        public int TotalPrice { get; set; }
        public bool Ispayed { get; set; }
        public bool Isdelivered { get; set; }

        public virtual ICollection<OrdereDetail> OrdereDetail { get; set; }
    }
}