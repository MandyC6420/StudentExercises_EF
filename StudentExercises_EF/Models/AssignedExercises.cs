namespace StudentExercises_EF.Models
{
    public class AssignedExercises
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int ExerciseId { get; set; }
        public Students Student { get; set; }
        public Exercises Exercise { get; set; }
    }
}
