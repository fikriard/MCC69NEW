using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    interface IJobs
    {
        List<Jobs> Get();
        Jobs GetId(int id);
        int Post(Jobs jobs);
        int Put(int id, Jobs jobs);
        int Delete(int id);
    }
}
