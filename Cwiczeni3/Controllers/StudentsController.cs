using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Cwiczeni3.Models;
using Cwiczeni3.Services;

namespace Tutorial_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        public List<Students> student = new List<Students>();

        [HttpGet]
        public IActionResult GetStudents()
        {
            IDbServices.Read(student);

            return Ok(student);
        }
        [HttpGet("idStudent")]
        public IActionResult GetStudent(string idStudent)
        {
            IDbServices.findByIdStudent(student, idStudent);

            return Ok(student);
        }
        [HttpPost]
        public IActionResult CreateStudent(Students student)
        {
            if (!Regex.IsMatch(student.idStudent, @"s[0-9]+")) 
            { 
                return BadRequest("invalid format");
            }
            this.student.Add(student);

            IDbServices.Save(this.student, false);

            return Ok(student);
        }

        [HttpPut("idStudent")]

        public IActionResult UpdateStudent(string idStudent, Students student)
        {
            this.student = IDbServices.Update(student, idStudent);

            IDbServices.Save(this.student, true);

            return Ok(student);
        }
        [HttpDelete("idStudent")]
        public IActionResult DeleteStudent(string idStudent)
        {
            IDbServices.Delete(student, idStudent);

            IDbServices.Save(student, true);

            return Ok(student);
        }
    }
}
