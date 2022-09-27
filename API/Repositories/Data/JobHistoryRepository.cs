using API.Context;
using API.Models;
using API.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Data
{
    public class JobHistoryRepository : IJobHistory
    {
        MyContext myContext;
        public JobHistoryRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<JobHistory> Get()
        {
            var data = myContext.JobHistories.ToList();
            return data;
        }

        public JobHistory GetId(int id)
        {
            var jobHistory = myContext.JobHistories.FirstOrDefault(c => c.Employee_Id.Equals(id));
            return jobHistory;
        }

        public int Post(JobHistory jobHistory)
        {
            myContext.JobHistories.Add(jobHistory);
            int result = myContext.SaveChanges();
            return result;
        }

        public int Put(int id, JobHistory jobHistory)
        {
            var data = myContext.JobHistories.Find(id);
            if (data == null)
            {
                return -1;
            }

            data.StartDate = jobHistory.StartDate;
            data.EndDate = jobHistory.EndDate;
            data.Department_Id = jobHistory.Department_Id;
            data.Job_Id = jobHistory.Job_Id;
            var result = myContext.SaveChanges();
            return result;
        }
    }
}
