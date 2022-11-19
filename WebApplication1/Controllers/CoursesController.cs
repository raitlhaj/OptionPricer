using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CoursesController : ControllerBase
    {
        private static readonly string[] courses = new[]{"Python","C#","C++"};

        private readonly ILogger<CoursesController> _logger;

        public CoursesController(ILogger<CoursesController> logger)
        {
            _logger = logger;
        }
        
        [HttpGet(Name = "GetCourses")]
        public string Get()
        {
            return "Something";
         
        }

        [HttpPost(Name = "PostNewCourses")]
        public string Get(string newC, [FromBody]List<int> all )
        {
            string ch =String.Empty;
            for (int i = 0; i < all.Count; i++)
            {
                int a = all[i];
                ch+= "New course has been posted: "+newC+" * "+a+"\n";
            }
            return ch;
        }
    }
}