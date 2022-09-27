using API.Context;
using API.Models;
using API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class EmployeesRepository : IEmployees
    {
        MyContext myContext;
        public EmployeesRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(int id)
        {
            var data = myContext.Employees.Find(id);
            if (data == null)
            {
                return -1;
            }

            myContext.Employees.Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public List<Employees> Get()
        {
            var data = myContext.Employees.ToList();
            return data;
        }

        public Employees GetId(int id)
        {
            var Employees = myContext.Employees.FirstOrDefault(c => c.Id.Equals(id));
            return Employees;
        }

        public int Post(Employees employees)
        {
            myContext.Employees.Add(employees);
            int result = myContext.SaveChanges();
            return result;
        }

        public int Put(int id, Employees employees)
        {
            var data = myContext.Employees.Find(id);
            if (data == null)
            {
                return -1;
            }

            data.FirstName = employees.FirstName;
            data.LastName = employees.LastName;
            data.Email = employees.Email;
            data.PhoneNumber = employees.PhoneNumber;
            data.Salary = employees.Salary;
            data.Department_Id = employees.Department_Id;
            data.Manager_Id = employees.Manager_Id;
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
