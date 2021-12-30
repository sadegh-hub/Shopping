using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Shapping.Models
{
    public class Subgroupkala
    {
        [Key]
        public int ID { get; set; }
        [Display(Name ="نام زیرگروه")]
        public string Name { get; set; }
        public int IDGroup { get; set; }
        [ForeignKey("IDGroup")]
        public virtual Groupkala Groupkala { get; set; }
        public virtual ICollection<Productkala> Productkala { get; set; }

    }
}