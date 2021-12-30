using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shapping.Models
{
    public class OrdereDetail
    {
        public int ID { get; set; }
        public int OrderId { get; set; }
        public int ProductID { get; set; }
        public int Quntity { get; set; }
        [ForeignKey("OrderId")]
        public  virtual Order Order { get; set; }
        [ForeignKey("ProductID")]
        public  virtual Productkala Productkala { get; set; }
    }
}