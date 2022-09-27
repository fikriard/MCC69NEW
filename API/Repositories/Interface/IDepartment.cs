using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    interface IDepartment
    {
        List<Departments> Get();
        Departments GetId(int id);
        int Post(Departments departments);
        int Put(int id, Departments departments);
        int Delete(int id);
    }
}
