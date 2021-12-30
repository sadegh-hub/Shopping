using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shapping.Models
{
    public class Productkala
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "نام کالا")]
        public string Name { get; set; }
        [Display(Name = "قیمت")]
        public int Price { get; set; }
        [Display(Name = "تصویر")]

        public string Image { get; set; }
        [Display(Name = "توضیحات")]
        public string Discription { get; set; }
        public int IDSubGroup { get; set; }
        [ForeignKey("IDSubGroup")]
        public virtual  Subgroupkala Subgroupkala { get; set; }

    }
}