using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    interface IEmployees
    {
        List<Employees> Get();
        Employees GetId(int id);
        int Post(Employees employees);
        int Put(int id, Employees employees);
        int Delete(int id);
    }
}
