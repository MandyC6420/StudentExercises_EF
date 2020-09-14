using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace StudentExercises_EF.Models.ViewModels
{
    public class StudentCohortViewModel
    {
        public Students student { get; set; }

        public List<SelectListItem> cohorts { get; set; } = new List<SelectListItem>();
    }
}
