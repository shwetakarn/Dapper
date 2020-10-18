using DapperDemo.Model;
using DapperDemo.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DapperDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
      private readonly IEmployeeRepository _employeeRepo;

      public EmployeeController(IEmployeeRepository employeeRepo)
     {
        _employeeRepo = employeeRepo;
     }
     
     [HttpGet]
    [Route("dob/{dateOfBirth}")]
    public async Task<ActionResult<List<Employee>>> GetByID(DateTime dateOfBirth)
    {
        return await _employeeRepo.GetByDateOfBirth(dateOfBirth);
    }

    [HttpPost]
    public int PostEmployee([FromBody]Employee emp)
     {
       return _employeeRepo.AddEmployee(emp);
     }

 [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<Employee>> GetByID(int id)
    {
        return await _employeeRepo.GetByID(id);
    }

    

    }
       
    }
