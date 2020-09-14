using Microsoft.EntityFrameworkCore;
using StudentExercises_EF.Models;

namespace StudentExercises_EF.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Students> Students { get; set; }

        public DbSet<Instructors> Instructor { get; set; }

        public DbSet<Exercises> Exercises { get; set; }
        public DbSet<Cohorts> Cohorts { get; set; }
        public DbSet<AssignedExercises> AssignedExercises { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cohorts>().HasMany(cohort => cohort.assignedStudents)
                    .WithOne(student => student.Cohort)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

//            builder.Entity<Cohorts>().HasData(
//              new Cohorts()
//              {
//                  Id = 1,
//                  Name = "Cohort One"
//              },
//              new Cohorts()
//              {
//                  Id = 2,
//                  Name = "Cohort Two"
//              }
//              );

//            builder.Entity<Instructors>().HasData(
//                new Instructors()
//                {
//                    Id = 1,
//                    FirstName = "Steve",
//                    LastName = "Adams",
//                    SlackHandle = "Steve-O",
//                    Specialty = "forgetting methods",
//                    CohortId = 2
//                },
//                new Instructors()
//                {
//                    Id = 2,
//                    FirstName = "Tommy",
//                    LastName = "Spurlock",
//                    SlackHandle = "Go Spurs",
//                    Specialty = "Seed Data",
//                    CohortId = 1
//                }
//                );

//            builder.Entity<Students>().HasData(
//               new Students()
//               {
//                   Id = 1,
//                   FirstName = "Gabby",
//                   LastName = "Guzman",
//                   SlackHandle = "Gabby Gab",
//                   CohortId = 2
//               },
//               new Students()
//               {
//                   Id = 2,
//                   FirstName = "Pat",
//                   LastName = "Miller",
//                   SlackHandle = "Patti",
//                   CohortId = 1
//               }
//               );
//            builder.Entity<Exercises>().HasData(
//               new Exercises()
//               {
//                   Id = 1,
//                   ExerciseName = "Calculator",
//                   ExerciseLanguage = "Javascript"
//               },
//               new Exercises()
//               {
//                   Id = 2,
//                   ExerciseName = "Battle of the Bands",
//                   ExerciseLanguage = "React.js"
//               }
//               );

//            builder.Entity<AssignedExercises>().HasData(
//              new AssignedExercises()
//              {
//                  Id = 1,
//                  StudentId = 1,
//                  ExerciseId = 1
//              },
//              new AssignedExercises()
//              {
//                  Id = 2,
//                  StudentId = 2,
//                  ExerciseId = 2
//              }
//              );
//        }
//    };
//}
