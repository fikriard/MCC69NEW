using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }

        public virtual Jobs Jobs { get; set; }
        [ForeignKey("Jobs")]
        public int Job_Id { get; set; }

        public int Salary { get; set; }

        public virtual Employees Manager { get; set; }
        [ForeignKey("Manager")]
        public int? Manager_Id { get; set; }

        public virtual Departments Departments { get; set; }
        [ForeignKey("Departments")]
        public int Department_Id { get; set; }
    }
}
