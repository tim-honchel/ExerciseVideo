using ExerciseVideo.Data;
using ExerciseVideo.Data.Entities;
using ExerciseVideo.Data.Repositories;
using ExerciseVideo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ExerciseVideo.Pages
{
    [Authorize]
    public class CreateWorkoutModel : PageModel
    {
        private UserService Service { get; set; }
        private int UserId{ get; set; }
        public WorkoutRepository Repository { get; set; }
        public CreateWorkoutModel(UserService userService, WorkoutRepository workoutRepository)
        {
            Service = userService;
            Repository = workoutRepository;
        }
        public async void OnGetAsync()
        {
            UserId = (await Service.GetCurrentUser(HttpContext)).Id;
        }

        public async Task<JsonResult> OnPostNewWorkout(WorkoutDto newWorkout)
        {
            var response = new BaseResponse()
            {
                Success = false
            };

            UserId = (await Service.GetCurrentUser(HttpContext)).Id;

            string validationErrors = ValidateWorkout(newWorkout);

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

            Repository.AddWorkout(workout);

            response.Success = true;

            return new JsonResult(response);
        }

        private string ValidateWorkout(WorkoutDto newWorkout)
        {
            string validationErrorMessage = string.Empty;

            if (newWorkout == null)
            {
                validationErrorMessage = "Workout data is empty or was not received.";
                return validationErrorMessage;
            }

            if (newWorkout.Id != 0)
            {
                validationErrorMessage += "ID cannot contain a value.";
            }
            if (string.IsNullOrEmpty(newWorkout.Title))
            {
                validationErrorMessage += "The workout title is blank.";
            }
            if (!Regex.Match(newWorkout.Title, "^[a-zA-Z0-9-_'.,;?!#$%&* ]*$").Success)
            {
                validationErrorMessage += "The workout title is invalid.";
            }
            if (newWorkout.Exercises == null || newWorkout.Exercises.Count == 0)
            {
                validationErrorMessage += "The workout does not contain any exercises.";
            }
            if (newWorkout.TransitionTime < 0 || newWorkout.TransitionTime > 10)
            {
                validationErrorMessage += "Transition time value is invalid.";
            }

            return validationErrorMessage;
        }
    }
}