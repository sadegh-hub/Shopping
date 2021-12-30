using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shapping.Models
{
    public class Groupkala
    {
        [Key]
        public int ID { get; set; }
        [Display(Name ="نام گروه کالا")]
        public string Name { get; set; }
        public virtual ICollection<Subgroupkala> Subgroupkala { get; set; }

    }
}