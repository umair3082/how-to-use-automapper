using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Entities;
using WebApplication1.Services;
using System.Data.SqlClient;
using AutoMapper;
using WebApplication1.DTOs;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Route("api/Emp")]
    public class EmpController:ControllerBase
    {
        private readonly IEmp employees;
        private readonly ApplicationDBContext context;

        public EmpController(IEmp employees,ApplicationDBContext context)
        {
            this.employees = employees;
            this.context = context;
        }
        [HttpGet]
        public async Task<ActionResult< List<EmpEntity>>>  Get()
        {
            return await context.Emptbl.OrderBy(x=>x.name).ToListAsync();
            #region Other Tried Methods

            //return  employees.GetAllEmp();
            //await context.employees.ToListAsync();

            //context.Emptbl.ToListAsync();
            //return new List<EmpEntity>()
            //{
            //    new EmpEntity(){empid=1,name="Umair",address="FSD",Phone="23874928"}
            //};
            #endregion

        }
        //Get Data By ID
        [HttpGet("{id:int}")]
        public async Task<ActionResult<EmpDTO>> Get(int id)
        {
            //return employees.GetByID(id);
            
            var abc = await context.Emptbl.FirstOrDefaultAsync(x=>x.empid==id);

            if (abc==null)
            {
                NotFound();
            }
            return Mapper.Map<EmpDTO>(abc);
        }
        //Insert Data
        [HttpPost]
        public async Task<ActionResult>  Post([FromBody] EmpEntity emp)
        {
             
            context.Add(emp);
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id,[FromBody] EmpEntity emp)
        {
            //var genres = Mapper.Map<EmpEntity>(emp);
            //genres.empid = id;
            //context.Entry(emp).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            //await context.SaveChangesAsync();
            emp.empid = id;
            context.Entry(emp).State=EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var abc = await context.Emptbl.FirstOrDefaultAsync(x => x.empid == id);
            if (abc == null)
            {
               return NotFound();
            }
            context.Remove(abc);
            await context.SaveChangesAsync();
            return NoContent();
        }

            
    }
}
