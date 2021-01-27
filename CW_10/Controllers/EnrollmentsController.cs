using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CW_10.DTOs.Request;
using CW_10.Services;
using Microsoft.AspNetCore.Mvc;

namespace CW_10.Controllers
{
    
    [ApiController]
    public class EnrollmentsController : ControllerBase
    {
        public IDbService _service;
           

        public EnrollmentsController(IDbService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("api/enrollments")]
        public IActionResult EnrollStudent(EnrollStudentRequest request)
        {
            var response = _service.EnrollStudent(request);

            if (response.answer == "OK")
            {
                return Created(response.answer, response);
            }
            else
            {
                return BadRequest(response);
            }
        }

        [HttpPost]
        [Route("api/enrollments/promotions")]
        public IActionResult StudentPromotion(StudentPromotionRequest request)
        {
            var response = _service.StudentPromotion(request);

            if (response.answer == "OK")
            {
                return Created(response.answer, response);
            }
            else
            {
                return NotFound(response);
            }
        }
    }
}
