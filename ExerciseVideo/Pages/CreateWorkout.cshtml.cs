using ExerciseVideo.Data.Entities;
using ExerciseVideo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ExerciseVideo.Pages
{
    [Authorize]
    public class CreateWorkoutModel : PageModel
    {
        private int UserId { get; set; }
        private UserService UserService { get; set; }

        public WorkoutService WorkoutService { get; set; }
        public CreateWorkoutModel(UserService userService, WorkoutService workoutService)
        {
            UserService = userService;
            WorkoutService = workoutService;
        }
        public async void OnGetAsync()
        {
            UserId = (await UserService.GetCurrentUser(HttpContext)).Id;
        }

        public async Task<JsonResult> OnPostNewWorkout(WorkoutDto newWorkout)
        {
            var response = new BaseResponse()
            {
                Success = false
            };

            UserId = (await UserService.GetCurrentUser(HttpContext)).Id;

            string validationErrors = WorkoutService.ValidateWorkout(newWorkout);

            if (!string.IsNullOrEmpty(validationErrors))
            {
                response.Message = validationErrors;
                return new JsonResult(response);
            }

            var settings = new WorkoutSettings()
            {
                AudioEndExercise = newWorkout.AudioEndExercise,
                AudioSecondTick = newWorkout.AudioSecondTick,
                AudioSpeakExercise = newWorkout.AudioSpeakExercise,
                AudioSpeakGo = newWorkout.AudioSpeakGo,
                TransitionTime = newWorkout.TransitionTime
            };

            var workout = new Workout()
            {
                Title = newWorkout.Title,
                Active = true,
                UserId = UserId,
                ExercisesJson = JsonConvert.SerializeObject(newWorkout.Exercises),
                SettingsJson = JsonConvert.SerializeObject(settings)
            };

            WorkoutService.AddWorkout(workout);

            response.Success = true;

            return new JsonResult(response);
        }
    }
}