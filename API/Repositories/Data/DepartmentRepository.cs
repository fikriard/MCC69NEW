using API.Context;
using API.Models;
using API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class DepartmentRepository : IDepartment
    {
        MyContext myContext;
        public DepartmentRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(int id)
        {
            var data = myContext.Departments.Find(id);
            if (data == null)
            {
                return -1;
            }

            myContext.Departments.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Departments> Get()
        {
            var data = myContext.Departments.ToList();
            return data;
        }

        public Departments GetId(int id)
        {
            var region = myContext.Departments.FirstOrDefault(c => c.Id.Equals(id));
            return region;
        }

        public int Post(Departments departments)
        {
            myContext.Departments.Add(departments);
            int result = myContext.SaveChanges();
            return result;
        }

        public int Put(int id, Departments departments)
        {
            var data = myContext.Departments.Find(id);
            if (data == null)
            {
                return -1;
            }

            data.Name = departments.Name;
            data.Location_Id = departments.Location_Id;
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
