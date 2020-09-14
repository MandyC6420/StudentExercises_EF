﻿using System.ComponentModel.DataAnnotations;

namespace StudentExercises_EF.Models
{
    public class Instructors
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 3)]
        public string SlackHandle { get; set; }
        public string Specialty { get; set; }
        public int CohortId { get; set; }
        public Cohorts Cohort { get; set; }

    }
}