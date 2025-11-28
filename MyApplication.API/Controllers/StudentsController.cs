using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MyApplication.API.Controllers;

// url: https://localhost:[portnumber]/api/students

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{

   // Get: https://localhost:[portnumber]/api/students/GetAllStudents
   [HttpGet("allstudents")]
   public IActionResult GetAllStudents()
   {
      string[] studentNames = new string[] { "John", "Jane", "Mark", "Emily", "David" };

      return Ok(studentNames);
   }


}
