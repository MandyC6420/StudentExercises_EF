using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace StudentExercises_EF.Models.ViewModels
{
    public class InstructorCohortViewModel
    {
        public Instructors instructor { get; set; }

        public List<SelectListItem> cohorts { get; set; } = new List<SelectListItem>();
    }
}
