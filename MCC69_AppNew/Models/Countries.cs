using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MCC69_AppNew.Models
{
    public class Countries
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Region Region { get; set; }
        [ForeignKey("Region")]
        public int Region_Id { get; set; }
    }
}
