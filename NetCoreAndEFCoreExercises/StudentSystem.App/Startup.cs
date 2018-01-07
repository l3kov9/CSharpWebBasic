namespace StudentSystem.App
{
    using System;
    using StudentSystem.Data;
    using System.Collections.Generic;
    using StudentSystem.Models;
    using System.Linq;
    using StudentSystem.Models.Enums;

    public class Startup
    {
        public static void Main()
        {
            var db = new StudentSystemDbContext();

            using (db)
            {
                // SeedData(db);

                // PrintData(db);
            }
        }

        private static void PrintData(StudentSystemDbContext db)
        {
            PrintStudentsAndTheirHomeworks(db);

            PrintCoursesWithTheirResources(db);

            PrintAllCoursesWithMoreThanFiveResources(db);

            PrintAllCoursesActiveOnAGivenDate(db);

            PrintStudentsAndTheirCourses(db);

            PrintCoursesWithResourcesAndLicenses(db);

            PrintStudentData(db);
        }

        private static void PrintStudentData(StudentSystemDbContext db)
        {
            var students = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    TotalCourses = s.Courses.Count,
                    TotalResources = s.Courses.Sum(c => c.Course.Resources.Count),
                    TotalLicenses = s.Courses.Sum(c => c.Course.Resources.Sum(r => r.Licenses.Count))
                })
                .OrderByDescending(s => s.TotalCourses)
                .ThenByDescending(s => s.TotalResources)
                .ThenBy(s => s.Name);

            foreach (var student in students)
            {
                var name = student.Name;
                var courses = student.TotalCourses;
                var resources = student.TotalResources;
                var licenses = student.TotalLicenses;

                Console.WriteLine($"{name}: {courses} courses, {resources} resources, {licenses} licenses.");
            }
        }

        private static void PrintCoursesWithResourcesAndLicenses(StudentSystemDbContext db)
        {
            var courses = db
                .Courses
                .OrderByDescending(c => c.Resources.Count)
                .ThenBy(c => c.Name)
                .Select(c => new
                {
                    c.Name,
                    Resources = c.Resources.Select(r => new
                    {
                        r.Name,
                        Licenses = r.Licenses.Select(l => new
                        {
                            l.Name
                        })
                        .ToList()
                    })
                    .OrderByDescending(r => r.Licenses.Count)
                    .ThenBy(r => r.Name)
                    .ToList()
                })
                .ToList();

            foreach (var course in courses)
            {
                var name = course.Name;
                var resources = course.Resources;

                Console.WriteLine(name);

                foreach (var resource in resources)
                {
                    Console.WriteLine($"    {resource.Name}");

                    foreach (var license in resource.Licenses)
                    {
                        Console.WriteLine($"    ---- {license.Name}");
                    }
                }
            }
        }

        private static void PrintStudentsAndTheirCourses(StudentSystemDbContext db)
        {
            var students = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    s.Courses.Count,
                    TotalPrice = s.Courses.Sum(c => c.Course.Price),
                    AveragePrice = s.Courses.Average(c => c.Course.Price)
                })
                .OrderByDescending(s => s.TotalPrice)
                .ThenByDescending(s => s.Count)
                .ThenBy(s => s.Name);

            foreach (var student in students)
            {
                var name = student.Name;
                var totalCourses = student.Count;
                var totalPrice = student.TotalPrice;
                var avgPrice = student.AveragePrice;

                Console.WriteLine($"{name} has {totalCourses} courses... {totalPrice}, avg - {avgPrice}");
            }
        }

        private static void PrintAllCoursesActiveOnAGivenDate(StudentSystemDbContext db)
        {
            var testingDate = DateTime.Now.AddDays(-400);

            var courses = db
                .Courses
                .Where(c => c.EndDate > testingDate && c.StartDate < testingDate)
                .Select(c => new
                {
                    c.Name,
                    c.StartDate,
                    c.EndDate,
                    CourseDuration = 0, // (((DateTime)c.EndDate - c.StartDate)).TotalDays,
                    StudentsEnrolled = c.Students.Count
                })
                .OrderByDescending(c => c.StudentsEnrolled)
                .ThenBy(c => c.CourseDuration)
                .ToList();

            foreach (var course in courses)
            {
                var name = course.Name;
                var startDate = course.StartDate;
                var endDate = course.EndDate;
                var courseDuration = course.CourseDuration;
                var totalStudents = course.StudentsEnrolled;

                Console.WriteLine($"{name} - {startDate:dd/mmm/yyyy} - {endDate:dd/mmm/yyyy} - {courseDuration:dd} days, total students - {totalStudents}");
            }
        }

        private static void PrintAllCoursesWithMoreThanFiveResources(StudentSystemDbContext db)
        {
            var courses = db
                .Courses
                .Where(c => c.Resources.Count > 5)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    c.Name,
                    Resources = c.Resources.Count
                })
                .ToList();

            foreach (var course in courses)
            {
                var name = course.Name;
                var totalResources = course.Resources;

                Console.WriteLine($"Course {name} has {totalResources} resources.");
            }
        }

        private static void PrintCoursesWithTheirResources(StudentSystemDbContext db)
        {
            var courses = db
                .Courses
                .OrderBy(c => c.StartDate)
                .ThenByDescending(c => c.EndDate)
                .Select(c => new
                {
                    c.Name,
                    c.Description,
                    Resources = c.Resources.ToList()
                })
                .ToList();

            foreach (var course in courses)
            {
                var name = course.Name;
                var description = course.Description;
                var resources = course.Resources;

                Console.WriteLine($"{name} - {description}");

                foreach (var resource in resources)
                {
                    Console.WriteLine($"    ---- {resource.Type} - {resource.Url}");
                }
            }
        }

        private static void PrintStudentsAndTheirHomeworks(StudentSystemDbContext db)
        {
            var students = db
                .Students
                .Select(s => new
                {
                    s.Name,
                    Homeworks = s.Homeworks.Select(h => new
                    {
                        h.Content,
                        h.ContentType
                    })
                        .ToList()
                })
                .ToList();

            foreach (var student in students)
            {
                var name = student.Name;
                var homeworks = student.Homeworks;

                Console.WriteLine(name);

                foreach (var homework in homeworks)
                {
                    Console.WriteLine($"    ---- {homework.Content} - {homework.ContentType}");
                }
            }
        }

        private static void SeedData(StudentSystemDbContext db)
        {
            SeedStudents(db);

            SeedCourses(db);

            SeedResources(db);

            SeedHomeworks(db);

            SeedStudentCourses(db);

            SeedLicenses(db);
        }

        private static void SeedLicenses(StudentSystemDbContext db)
        {
            var totalLicenses = 100;

            var rnd = new Random();

            var resourceIds = db
                .Resources
                .Select(r => r.Id)
                .ToList();

            var licenses = new List<License>();

            for (int i = 0; i < totalLicenses; i++)
            {
                var license = new License
                {
                    Name = $"License {rnd.Next(10000, 99999)}",
                    ResourceId = resourceIds[rnd.Next(0, resourceIds.Count)]
                };

                licenses.Add(license);
            }

            db
                .Licenses
                .AddRange(licenses);

            db.SaveChanges();
        }

        private static void SeedStudentCourses(StudentSystemDbContext db)
        {
            const int totalCoursesPerStudent = 10;

            var rnd = new Random();

            var studentIds = db
                .Students
                .Select(s => s.Id)
                .ToList();

            var coursesId = db
                .Courses
                .Select(c => c.Id)
                .ToList();

            for (int i = 0; i < studentIds.Count; i++)
            {
                for (int j = 0; j < totalCoursesPerStudent; j++)
                {
                    var student = db
                        .Students
                        .Where(s => s.Id == studentIds[i])
                        .FirstOrDefault();

                    var course = new StudentCourse
                    {
                        CourseId = coursesId[rnd.Next(0, coursesId.Count)]
                    };

                    if (student.Courses.Any(c => c.CourseId == course.CourseId))
                    {
                        continue;
                    }

                    db
                        .Students
                        .Where(s => s.Id == student.Id)
                        .FirstOrDefault()
                        .Courses
                        .Add(course);

                    db.SaveChanges();
                }
            }
        }

        private static void SeedHomeworks(StudentSystemDbContext db)
        {
            const int totalHomeworks = 500;

            var rnd = new Random();

            var studentIds = db
                .Students
                .Select(s => s.Id)
                .ToList();

            var courseIds = db
                .Courses
                .Select(c => c.Id)
                .ToList();

            var homeworks = new List<Homework>();

            for (int i = 0; i < totalHomeworks; i++)
            {
                var homework = new Homework
                {
                    Content = $"Content number {i + 1}",
                    ContentType = HomeworkContentType.application,
                    SubmissionDate = DateTime.Now.AddDays(rnd.Next(-1000, 0)),
                    StudentId = studentIds[rnd.Next(0, studentIds.Count)],
                    CourseId = courseIds[rnd.Next(0, courseIds.Count)]
                };

                homeworks.Add(homework);
            }

            db
                .Homeworks
                .AddRange(homeworks);

            db.SaveChanges();
        }

        private static void SeedResources(StudentSystemDbContext db)
        {
            const int totalResources = 100;

            var rnd = new Random();

            var courseIds = db
                .Courses
                .Select(c => c.Id)
                .ToList();

            var resources = new List<Resource>();

            for (int i = 0; i < totalResources; i++)
            {
                var resource = new Resource
                {
                    Name = $"Resource {i + 1}",
                    Type = ResourceType.document,
                    Url = $"www.urlnumber{i + 10000}.com",
                    CourseId = courseIds[rnd.Next(0, courseIds.Count)]
                };

                resources.Add(resource);
            }

            db
                .Resources
                .AddRange(resources);

            db.SaveChanges();
        }

        private static void SeedCourses(StudentSystemDbContext db)
        {
            const int totalCourses = 10;

            var currentDate = DateTime.Now;
            var rnd = new Random();

            var courses = new List<Course>();

            for (int i = 0; i < totalCourses; i++)
            {
                var course = new Course
                {
                    Name = $"Course {(i + 1)}",
                    Description = i % 2 == 0 ? $"DescriptionNumber {i}" : null,
                    StartDate = currentDate.AddDays(rnd.Next(-1000, -300)),
                    EndDate = i % 3 == 0 ? default(DateTime) : currentDate.AddHours(rnd.Next(-300, -1)),
                    Price = decimal.Parse($"{rnd.Next(0, 5)}{rnd.Next(0, 10)}.{rnd.Next(0, 10)}{rnd.Next(0, 10)}")
                };

                courses.Add(course);
            }

            db
                .Courses
                .AddRange(courses);

            db.SaveChanges();
        }

        private static void SeedStudents(StudentSystemDbContext db)
        {
            const int totalStudents = 50;

            var currentDate = DateTime.Now;
            var rnd = new Random();

            var students = new List<Student>();

            for (int i = 0; i < totalStudents; i++)
            {
                var student = new Student
                {
                    Name = $"Student {i + 1}",
                    PhoneNumber = $"{rnd.Next(100000, 999999)}",
                    RegistrationDate = currentDate.AddDays(rnd.Next(-5000, -100)),
                    Birthday = currentDate.AddYears(rnd.Next(-40, -18))
                };

                students.Add(student);
            }

            db
                .Students
                .AddRange(students);

            db.SaveChanges();
        }
    }
}
