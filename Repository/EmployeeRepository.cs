using System.Collections.Generic;
using System.Threading.Tasks;
using DapperDemo.Model;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System;

namespace DapperDemo.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IConfiguration _config;
        public EmployeeRepository(IConfiguration config)
        {
            this._config = config;
        }

        public IDbConnection connection
        {
            get{
                return new SqlConnection(_config.GetConnectionString("MyConnectionString"));
            }
        }
        public int AddEmployee(Employee emp)
        {
             using(IDbConnection conn = connection) 
            {
                
                conn.Open();
                IDbTransaction sqltrans = conn.BeginTransaction();
                var param = new DynamicParameters();
                param.Add("@Fname",emp.FirstName);
                param.Add("@Lname",emp.LastName);
                param.Add("@Dob",emp.DateOfBirth);

                var result = conn.Execute("AddEmployee",param,sqltrans,0,System.Data.CommandType.StoredProcedure);
                
                if(result >0)
                {
                    sqltrans.Commit();
                }
                else{
                    sqltrans.Rollback();
                }
                return  result;
            }
        }

        public async Task<List<Employee>> GetByDateOfBirth(DateTime dateOfBirth)
        {
            using(IDbConnection conn = connection) 
            {
                string query = "select * from Employee where DateOfBirth = @DateOfBirth";
                conn.Open();
                var result = await conn.QueryAsync<Employee>(query, new {DateOfBirth = dateOfBirth});
                return  result.ToList();
            }
        }

        public async Task<Employee> GetByID(int id)
        {
            using(IDbConnection conn = connection) 
            {
                string query = "select * from Employee where ID = @ID";
                conn.Open();
                var result = await conn.QueryAsync<Employee>(query, new {ID = id});
                return  result.FirstOrDefault();
            }
        }
    }
}