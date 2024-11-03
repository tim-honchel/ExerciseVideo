using ExerciseVideo.Data.Entities;
using ExerciseVideo.Data.Repositories;
using ExerciseVideo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ExerciseVideo.Pages
{
    [Authorize]
    public class WorkoutModel : PageModel
    {
        public bool AudioEndExercise;
        public bool AudioSecondTick;
        public bool AudioSpeakExercise;
        public bool AudioSpeakGo;
        public Exercise[]? Exercises;
        public int TransitionTime;

        private UserService UserService { get; set; }
        private WorkoutService WorkoutService { get; set; }
        private int UserId { get; set; }

        public WorkoutModel(UserService userService, WorkoutService workoutService)
        {
            UserService = userService;
            WorkoutService = workoutService;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            WorkoutDto workout = new WorkoutDto();
            
            if (id == null || id < -1)
            {
                return Redirect("Error");
            }

            UserId = (await UserService.GetCurrentUser(HttpContext)).Id;

            if (id == -1)
            {
                workout = WorkoutService.GetSampleWorkout();
            }
            else
            {
                var rawWorkout = WorkoutService.GetWorkoutById((int)id);
                if (rawWorkout == null || rawWorkout.UserId != UserId)
                {
                    return Redirect("Error");
                }

                workout = WorkoutService.ConvertWorkoutToWorkoutDto(rawWorkout);
            }
            
            Exercises = workout.Exercises.ToArray();
            AudioEndExercise = workout.AudioEndExercise;
            AudioSecondTick = workout.AudioSecondTick;
            AudioSpeakExercise = workout.AudioSpeakExercise;
            AudioSpeakGo = workout.AudioSpeakGo;
            TransitionTime = workout.TransitionTime;

            return Page();
        }
    }
}
