using API.Context;
using API.Models;
using API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class LocationRepository : ILocation
    {
        MyContext myContext;
        public LocationRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(int id)
        {
            var data = myContext.Locations.Find(id);
            if (data == null)
            {
                return -1;
            }

            myContext.Locations.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Locations> Get()
        {
            var data = myContext.Locations.ToList();
            return data;
        }

        public Locations GetId(int id)
        {
            var locations = myContext.Locations.FirstOrDefault(c => c.Id.Equals(id));
            return locations;
        }

        public int Post(Locations locations)
        {
            myContext.Locations.Add(locations);
            int result = myContext.SaveChanges();
            return result;
        }

        public int Put(int id, Locations locations)
        {
            var data = myContext.Locations.Find(id);
            if (data == null)
            {
                return -1;
            }

            data.StreetAddress = locations.StreetAddress;
            data.PostalCode = locations.PostalCode;
            data.City = locations.City;
            data.Country_Id = locations.Country_Id;

            var result = myContext.SaveChanges();
            return result;
        }
    }
}
