using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        List<Student> students;
        List<Course> Courses;

        public RegistrationController()
        {
            students = GetStudents();
            Courses = GetCourses();
        }

        private List<Student> GetStudents()
        {
            var getData = new SettingController();
            var listStudent = getData.Get10Student();
            if (listStudent is OkObjectResult student)
            {
                return student.Value as List<Student>;
            }
            return new List<Student>();
        }
        private List<Course> GetCourses()
        {

            var getData = new SettingController();
            var listCourses = getData.GetCourses();
            if (listCourses is OkObjectResult course)
            {
                return course.Value as List<Course>;
            }
            return new List<Course>();
        }

        [HttpPost]
        public IActionResult Register(Registration registration)
        {
            var student = students.Find(c => c.Id == registration.StudentId);
            var course = Courses.Find(c => c.Id == registration.CourseId);
            if (student == null || course == null)
            {
                return BadRequest(new { ErrorOn = (student == null ? "student id" : "course id") });
            }

            //register 

            return Ok(registration);
        }
    }
}
