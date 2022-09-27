using API.Context;
using API.Models;
using API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class CountryRepository : ICountry
    {
        MyContext myContext;
        public CountryRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(int id)
        {
            var data = myContext.Countries.Find(id);
            if (data == null)
            {
                return -1;
            }

            myContext.Countries.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Countries> Get()
        {
            var data = myContext.Countries.ToList();
            return data;
        }

        public Countries GetId(int id)
        {
            var country = myContext.Countries.FirstOrDefault(c => c.Id.Equals(id));
            return country;
        }

        public int Post(Countries countries)
        {
            myContext.Countries.Add(countries);
            int result = myContext.SaveChanges();
            return result;
        }

        public int Put(int id, Countries countries)
        {
            var data = myContext.Countries.Find(id);
            if (data == null)
            {
                return -1;
            }

            data.Name = countries.Name;
            data.Region_Id = countries.Region_Id;

            var result = myContext.SaveChanges();
            return result;
        }
    }
}
