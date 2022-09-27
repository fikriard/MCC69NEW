using API.Context;
using API.Models;
using API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class JobsRepository : IJobs
    {
        MyContext myContext;
        public JobsRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(int id)
        {
            var data = myContext.Jobs.Find(id);
            if (data == null)
            {
                return -1;
            }

            myContext.Jobs.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Jobs> Get()
        {
            var data = myContext.Jobs.ToList();
            return data;
        }

        public Jobs GetId(int id)
        {
            var jobs = myContext.Jobs.FirstOrDefault(c => c.Id.Equals(id));
            return jobs;
        }

        public int Post(Jobs jobs)
        {
            myContext.Jobs.Add(jobs);
            int result = myContext.SaveChanges();
            return result;
        }

        public int Put(int id, Jobs jobs)
        {
            var data = myContext.Jobs.Find(id);
            if (data == null)
            {
                return -1;
            }

            data.Title = jobs.Title;
            data.MinSalary = jobs.MinSalary;
            data.MaxSalary = jobs.MaxSalary;
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
