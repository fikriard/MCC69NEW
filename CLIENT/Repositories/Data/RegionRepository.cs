using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLIENT.Repositories.Data
{
    public class RegionRepository : GeneralRepository<Region>
    {
        public RegionRepository(string request = "Region/") : base(request)
        {

        }
    }
}
