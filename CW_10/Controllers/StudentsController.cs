using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW_10.Models;
using CW_10.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_10.Controllers
{
    
    [ApiController]
    
    public class StudentsController : ControllerBase
    {
        public IDbService _service;

        public StudentsController(IDbService service)
        {
            _service = service;
        }

        
        [HttpGet]
        [Route("api/students")]
        public IActionResult GetStudents()
        {
           // var db = new s16449Context();
            var result = _service.GetStudents();
            return Ok(result);
        }

        [HttpPost]
        [Route("api/students/create/")]
        public IActionResult CreateStudent(Student student)
        {
            var result = _service.CreateStudent(student);
            return Ok(result);
        }


        [HttpPost]
        [Route("/api/students/edit/{id}")]

        public IActionResult EditStudent(Student student, string id)
        {
            var result = _service.EditStudent(student, id);
            return Ok(result);
        }

        [HttpDelete]
        [Route("/api/students/delete/{id}")]
        public IActionResult DeleteStudent(string id)
        {
            var result = _service.DeleteStudent(id);
            return Ok(result);
        }
    }
}
