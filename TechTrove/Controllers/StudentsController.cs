using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechTrove.Models;
using TechTrove.DTO;
using Swashbuckle.AspNetCore.Annotations;
using TechTrove.UnitOfWork;
using Microsoft.AspNetCore.Authorization;

namespace TechTrove.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly Unit unit;
        public StudentsController(Unit unit)
        {
            this.unit = unit;
        }
        // GET: api/Students
        /// <summary>
        /// Method For Get All Students
        /// </summary>
        /// <param name="page">number of current page</param>
        /// <param name="pageSize">Number Of Content For Each Page</param>
        /// <returns>
        /// 200 -> if One Student Founded
        /// 404 -> if not Found Any Student
        /// </returns>
        //[HttpGet]
        //[Authorize]
        //public ActionResult GetStudents(int page = 1,int pageSize = 10)
        //{
        //    List<Student> sts = unit.StudRepo.GetAll(s => s.Dept,s => s.St_superNavigation);
        //    if (sts.Count == 0) return NotFound();
        //    int Count =sts.Count();
        //    var totalPages = (int)Math.Ceiling((double)Count / pageSize);
        //    sts=sts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        //    List<StudentDTO> stsdto = new List<StudentDTO>();
        //    foreach (var item in sts)
        //    {
        //        StudentDTO stu = new StudentDTO()
        //        {
        //            id = item.St_Id,
        //            fname=item.St_Fname??"",
        //            lname=item.St_Lname??"",
        //            address=item.St_Address?? "",
        //            age=item.St_Age??null,
        //            departmentName=item.Dept.Dept_Name??"",
        //            super = item.St_superNavigation?.St_Fname ?? ""
        //        };
        //        stsdto.Add(stu);
        //    }

        //    return Ok(new { students = stsdto, totalPages }); ;
        //}

        // GET: api/Students/5
        //[HttpGet("{id:int}")]
        //[Authorize]
        //[SwaggerOperation(Summary = "method to return student data", Description = "this method return student data")]
        //[SwaggerResponse(400, "if no student", Type = typeof(void))]
        //[SwaggerResponse(200, "if found any student", Type = typeof(StudentDTO))]
        //public ActionResult GetStudent(int id)
        //{
        //    Student s = unit.StudRepo.GetById(id, s => s.Dept, s => s.St_superNavigation);
        //    if (s == null) return NotFound();
        //    StudentDTO st = new StudentDTO()
        //    {
        //        id = s.St_Id,
        //        fname = s.St_Fname,
        //        lname = s.St_Lname,
        //        address = s.St_Address,
        //        age = s.St_Age,
        //        departmentName = s.Dept.Dept_Name,
        //        super = s.St_superNavigation?.St_Fname
        //    };
        //    return Ok(st);
        //}

        //[HttpGet("{name:alpha}")]
        //[Authorize]
        //public ActionResult Serach(string name, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        //{
        //    List<Student> sts = unit.StudRepo.GetByName(name, s => s.Dept, s => s.St_superNavigation); ;
        //    if (sts.Count == 0) return NotFound();
        //    int Count = sts.Count();
        //    var totalPages = (int)Math.Ceiling((double)Count / pageSize);
        //    sts = sts.Skip((page - 1) * pageSize).Take(pageSize).ToList();
        //    List<StudentDTO> stsdto = new List<StudentDTO>();
        //    foreach (var item in sts)
        //    {
        //        StudentDTO stu = new StudentDTO()
        //        {
        //            id = item.St_Id,
        //            fname = item.St_Fname,
        //            lname = item.St_Lname,
        //            address = item.St_Address,
        //            age = item.St_Age,
        //            departmentName = item.Dept.Dept_Name,
        //            super = item.St_superNavigation?.St_Fname
        //        };
        //        stsdto.Add(stu);
        //    }
        //    return Ok(stsdto);
        //}
        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //[Authorize]
        //public async Task<IActionResult> PutStudent(int id, Student student)
        //{
        //    if (id != student.St_Id)
        //    {
        //        return BadRequest();
        //    }
        //    unit.StudRepo.Update(student);
        //    try
        //    {
        //        unit.StudRepo.Save();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!StudentExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //    return NoContent();
        //}

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //[Authorize]
        //public async Task<ActionResult<Student>> PostStudent(Student student)
        //{
        //    unit.StudRepo.Add(student);
        //    try
        //    {
        //        unit.StudRepo.Save();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (StudentExists(student.St_Id))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetStudent", new { id = student.St_Id }, student);
        //}

        // DELETE: api/Students/5
        //[HttpDelete("{id}")]
        //[Authorize]
        //public async Task<IActionResult> DeleteStudent(int id)
        //{
        //    var student = unit.StudRepo.GetById(id, s => s.Dept, s => s.St_superNavigation);
        //    if (student == null)
        //    {
        //        return NotFound();
        //    }
        //    unit.StudRepo.Delete(id);
        //    unit.StudRepo.Save();
        //    return NoContent();
        //}

        //private bool StudentExists(int id)
        //{
        //    return unit.StudRepo.Exist(id);
        //}
    }
}
