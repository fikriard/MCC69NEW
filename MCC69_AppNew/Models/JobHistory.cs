using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MCC69_AppNew.Models
{
    public class JobHistory
    {
        
        
        [Key]
        [ForeignKey("Employees")]
        public int Employee_Id { get; set; }
        public virtual Employees Employees { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Jobs Jobs { get; set; }
        [ForeignKey("Jobs")]
        public int Job_Id { get; set; }

        public virtual Departments Department { get; set; }
        [ForeignKey("Department")]
        public int Department_Id { get; set; }
    }
}
