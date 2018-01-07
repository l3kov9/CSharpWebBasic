namespace StudentSystem.Data
{
    using Microsoft.EntityFrameworkCore;
    using StudentSystem.Data.EntityConfig;
    using StudentSystem.Data.ServeConfig;
    using StudentSystem.Models;

    public class StudentSystemDbContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }

        public DbSet<Homework> Homeworks { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<StudentCourse> StudentCourses { get; set; }

        public DbSet<License> Licenses { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
                .UseSqlServer(Configuration.ConfigString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new StudentConfig());

            builder
                .ApplyConfiguration(new StudentCourseConfig());

            builder
                .ApplyConfiguration(new CourseConfig());

            builder
                .ApplyConfiguration(new LicenseConfig());
        }
    }
}
