using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTrove.Models;
using TechTrove.DTO;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TechTrove.UnitOfWork;
using Microsoft.AspNetCore.Authorization;

namespace TechTrove.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly Unit unit;
        public DepartmentsController(Unit unit)
        {
            this.unit = unit;
        }

        // GET: api/Departments
        //[HttpGet]
        //[SwaggerOperation(Summary = "method to return all department data", Description = "this method return all department data")]
        //[SwaggerResponse(400, "if no department", Type = typeof(void))]
        //[SwaggerResponse(200, "if found any department", Type = typeof(List<DepartmentDTO>))]
        //public ActionResult GetDepartments()
        //{
        //    List<Department> deps = unit.DepartRepo.GetAll(d=>d.Students);
        //    if (deps.Count == 0) return NotFound();
        //    List<DepartmentDTO> depdto = new List<DepartmentDTO>();
        //    foreach (var item in deps)
        //    {
        //        DepartmentDTO dept = new DepartmentDTO()
        //        {
        //            id = item.Dept_Id,
        //            name=item.Dept_Name,
        //            desc=item.Dept_Desc,
        //            location=item.Dept_Location,
        //            studentCount=item.Students.Count,
        //        };
        //        depdto.Add(dept);
        //    }

        //    return Ok(depdto);
        //}

        // GET: api/Departments/5
        /// <summary>
        /// Method For Get Specific Department
        /// </summary>
        /// <param name="id">Id For Department That Search On It</param>
        /// <returns>
        /// 200 -> if founded
        /// 404 -> not founded
        /// </returns>
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult GetDepartment(int id)
        {
            //Department d = unit.DepartRepo.GetById(id,d => d.Students);
            //if (d == null) return NotFound();
            //DepartmentDTO dept = new DepartmentDTO()
            //{
            //    id = d.Dept_Id,
            //    name = d.Dept_Name,
            //    desc = d.Dept_Desc,
            //    location = d.Dept_Location,
            //    studentCount = d.Students.Count,
            //};
            return Ok(/*dept*/);
        }

        // PUT: api/Departments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutDepartment(int id/*, Department department*/)
        {
            //if (id != department.Dept_Id)
            //{
            //    return BadRequest();
            //}
            //unit.DepartRepo.Update(department);
            //try
            //{
            //    unit.DepartRepo.Save();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!DepartmentExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}
            return NoContent();
        }

        // POST: api/Departments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //[Authorize]
        //public async Task<ActionResult<Department>> PostDepartment(Department department)
        //{
        //    unit.DepartRepo.Add(department);
        //    try
        //    {
        //        unit.DepartRepo.Save();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (DepartmentExists(department.Dept_Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetDepartment", new { id = department.Dept_Id }, department);
        //}

        // DELETE: api/Departments/5
        //[HttpDelete("{id}")]
        //[Authorize]
        //public async Task<IActionResult> DeleteDepartment(int id)
        //{
        //    if (!DepartmentExists(id))
        //    {
        //        return NotFound();
        //    }
        //    unit.DepartRepo.Delete(id);
        //    unit.DepartRepo.Save();
        //    return NoContent();
        //}

        //private bool DepartmentExists(int id)
        //{
        //    return unit.DepartRepo.Exist(id);
        //}
    }
}
