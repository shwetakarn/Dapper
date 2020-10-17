using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DapperDemo.Model;

namespace DapperDemo.Repository
{
    public interface IEmployeeRepository
    {
         Task<Employee> GetById(int id);
         Task<List<Employee>> GetByDateOfBirth(DateTime dateOfBirth);
         int AddEmployee(Employee emp);

    }
}