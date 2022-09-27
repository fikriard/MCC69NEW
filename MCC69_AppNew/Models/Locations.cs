using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MCC69_AppNew.Models
{
    public class Locations
    {
        [Key]
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }

        public virtual Countries Countries { get; set; }
        [ForeignKey("Countries")]
        public int Country_Id { get; set; }
    }
}
