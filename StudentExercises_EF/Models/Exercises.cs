using System.Collections.Generic;

namespace StudentExercises_EF.Models
{
    public class Exercises
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; }
        public string ExerciseLanguage { get; set; }

        public List<AssignedExercises> assignedStudents { get; set; } = new List<AssignedExercises>();
    }
}
