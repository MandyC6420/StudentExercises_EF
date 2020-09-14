using System.Collections.Generic;

namespace StudentExercises_EF.Models
{
    public class Students
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string SlackHandle { get; set; }

        public int CohortId { get; set; }

        public Cohorts Cohort { get; set; }

        //public int Grade { get; set; }

        public List<AssignedExercises> assignedExercises { get; set; } = new List<AssignedExercises>();
    }
}
