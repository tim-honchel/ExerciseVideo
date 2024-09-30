using ExerciseVideo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseVideo.Pages
{
    [Authorize]
    public class CreateWorkoutModel : PageModel
    {
        public WorkoutDto NewWorkout { get; set; } = new WorkoutDto();
        public void OnGet()
        {

        }

    }
}
