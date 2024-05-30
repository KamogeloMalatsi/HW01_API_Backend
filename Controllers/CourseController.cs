using Architecture_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Architecture_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _courseRepository;

        public CourseController(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        [HttpGet]
        [Route("GetAllCourses")]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var results = await _courseRepository.GetAllCourseAsync();
                return Ok(results);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpGet]
        [Route("GetCourse/{courseId}")]

        public async Task<IActionResult> GetCourse(int courseId)
        {
            try
            {
                var result = await _courseRepository.GetCourseAsync(courseId);

                if (result == null)
                    return StatusCode(404, "Not Found");
                else
                    return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
        }

        [HttpPut]
        [Route("EditCourse/{courseId}")]

        public async Task<ActionResult<CourseViewModel>> EditCourse(CourseViewModel editModel, int courseId)
        {
            try
            {
                var result = await _courseRepository.GetCourseAsync(courseId);

                if (result == null)
                    return StatusCode(404, "Not Found, the requeste course does not exist");
                else
                {
                    result.Name = editModel.courseName;
                    result.Duration = editModel.courseDuration;
                    result.Description = editModel.courseDescription;

                }

                if (await _courseRepository.SaveChangesAsync())
                { return Ok(result); }
                //return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return StatusCode(400, "Bad Request, your request is invalid!");
        }

        [HttpDelete]
        [Route("DeleteCourse/{courseId}")]

        public async Task<IActionResult> DeleteCourse(int courseId)
        {
            try
            {
                var result = await _courseRepository.GetCourseAsync(courseId);

                if (result == null)
                    return StatusCode(404, "Not Found, the requested course does not exist");
                else
                {
                    _courseRepository.Delete(result);

                }

                if (await _courseRepository.SaveChangesAsync())
                {
                    return Ok(result);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return StatusCode(400, "Bad Request, your request is invalid!");
        }

        [HttpPost]
        [Route("AddCourse")]

        public async Task<IActionResult> AddCourse(CourseViewModel newModel)
        {
            try
            {
                var newCourse = new Course
                {
                    Name = newModel.courseName,
                    Duration = newModel.courseDuration,
                    Description = newModel.courseDescription
                };

                _courseRepository.Add(newCourse);


                if (await _courseRepository.SaveChangesAsync())
                {
                    return Ok(newCourse);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error. Please contact support.");
            }
            return StatusCode(400, "Bad Request, your request is invalid!");
        }

    }
}
