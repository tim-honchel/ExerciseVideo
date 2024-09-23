using ExerciseVideo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseVideo.Pages
{
    [Authorize]
    public class CreateWorkoutModel : PageModel
    {
        public Workout NewWorkout { get; set; } = new Workout();
        public void OnGet()
        {

        }

    }
}
