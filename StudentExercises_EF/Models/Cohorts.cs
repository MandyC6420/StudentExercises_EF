using System.Collections.Generic;

namespace StudentExercises_EF.Models
{
    public class Cohorts
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Students> assignedStudents { get; set; } = new List<Students>();

        public List<Instructors> InstructorList { get; set; } = new List<Instructors>();
    }
}
