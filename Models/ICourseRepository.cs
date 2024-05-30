namespace Architecture_API.Models
{
    public interface ICourseRepository
    {
        // Course
        Task<Course[]> GetAllCourseAsync();

        Task<Course> GetCourseAsync(int courseId);

        Task<bool> SaveChangesAsync();

        void Delete<T>(T entity) where T : class;

        void Add<T>(T entity) where T : class;

    }
}
