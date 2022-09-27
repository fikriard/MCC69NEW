using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories.Interface
{
    interface IJobHistory
    {
        List<JobHistory> Get();
        JobHistory GetId(int id);
        int Post(JobHistory jobHistory);
        int Put(int id, JobHistory jobHistory);
        int Delete(int id);
    }
}
