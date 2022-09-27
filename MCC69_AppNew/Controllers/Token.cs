using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MCC69_AppNew.Controllers
{
    public class Token
    {
        [Key]
        public string Key
        {
            get; set;
        }
    }
}
