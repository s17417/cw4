using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using cw4.Models;
using cw4.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace cw4.Controllers
{
    [Route("api/enrollment")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        IEnrollStudentDbService _service;
       
        public EnrollmentController(IEnrollStudentDbService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> enrollStudent([FromBody]EnrollStudentDTO enrollStudentDTO)
        {
            EnrollStudentDTO d= new EnrollStudentDTO();
            var enroll =  _service.EnrollStudent(enrollStudentDTO);
             if (enroll == null) return this.BadRequest();
            return CreatedAtRoute("Get", new { id = enrollStudentDTO.IndexNumber }, enroll);   
        }
    }
}