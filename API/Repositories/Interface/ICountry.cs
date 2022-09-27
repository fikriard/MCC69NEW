using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    public interface ICountry
    {
        List<Countries> Get();
        Countries GetId(int id);
        int Post(Countries countries);
        int Put(int id, Countries countries);
        int Delete(int id);
    }
}
