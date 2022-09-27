using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    public interface IRegion
    {
        List<Region> Get();
        Region GetId(int id);
        int Post(Region region);
        int Put(int id, Region region);
        int Delete(int id);
    }
}
