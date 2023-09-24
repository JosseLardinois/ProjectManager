using Microsoft.AspNetCore.Mvc;

namespace ProjectManager.Controllers
{
    [ApiController]
    [Route("controller")]
    public class ProjectController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetProject()
        {

            //make repo, interfaces, etc..
            return Ok();
        }

        [HttpGet]
        public IActionResult GetPhaseOfProject()
        {

            //make repo, interfaces, etc..
            return Ok();
        }

        [HttpGet]
        public IActionResult GetTasksOfProject()
        {

            //make repo, interfaces, etc..
            return Ok();
        }
    }
}
