using System.ComponentModel.DataAnnotations;

namespace Architecture_API.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Duration { get; set; } = String.Empty;

    }
}
