using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NZwalks.API.Controllers
{
    // https://localhost:portnumber/api/Students
    [Route("api/[controller]")]
    [ApiController]//This attribute tell the App that this is for api use
    public class StudentsController : ControllerBase
    {
        //this line tells that it is a GET action method and will follow 'https://localhost:portnumber/api/Students'
        [HttpGet]
        public IActionResult GetAllStudents()
        {
            string[] StudentsNames = new string[] { "ram", "shyam", "rahul", "sumit", "sunil" };

            return Ok(StudentsNames);
        }
    }
}
