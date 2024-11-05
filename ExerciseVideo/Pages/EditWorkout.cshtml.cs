using ExerciseVideo.Data.Entities;
using ExerciseVideo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Identity.Client.Extensibility;
using Newtonsoft.Json;

namespace ExerciseVideo.Pages
{
    [Authorize]
    public class EditWorkoutModel : PageModel
    {
        private UserService UserService { get; set; }
        private WorkoutService WorkoutService { get; set; }
        private int UserId { get; set; }
        public int WorkoutId { get; set; }
        public string Title { get; set; }
        public bool AudioEndExercise { get; set; }
        public bool AudioSecondTick { get; set; }
        public bool AudioSpeakExercise { get; set; }
        public bool AudioSpeakGo { get; set; }
        public Exercise[]? Exercises { get; set; }
        public int TransitionTime { get; set; }
        public EditWorkoutModel(UserService userService, WorkoutService workoutService)
        {
            UserService = userService;
            WorkoutService = workoutService;
        }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || id < -1)
            {
                return Redirect("Error");
            }
            UserId = (await UserService.GetCurrentUser(HttpContext)).Id;

            var rawWorkout = WorkoutService.GetWorkoutById((int)id);
            if (rawWorkout == null || rawWorkout.UserId != UserId)
            {
                return Redirect("Error");
            }

            var workout = WorkoutService.ConvertWorkoutToWorkoutDto(rawWorkout);

            WorkoutId = workout.Id;
            Title = workout.Title;
            Exercises = workout.Exercises.ToArray();
            AudioEndExercise = workout.AudioEndExercise;
            AudioSecondTick = workout.AudioSecondTick;
            AudioSpeakExercise = workout.AudioSpeakExercise;
            AudioSpeakGo = workout.AudioSpeakGo;
            TransitionTime = workout.TransitionTime;

            return Page();
        }

        public async Task<JsonResult> OnPutArchiveWorkout(int workoutId)
        {
            var response = new BaseResponse()
            {
                Success = false
            };

            var userId = (await UserService.GetCurrentUser(HttpContext)).Id;
            var rawWorkout = WorkoutService.GetWorkoutById((int)workoutId);

            if (rawWorkout == null || rawWorkout.UserId != userId)
            {
                response.Message = "Unable to update";
                return new JsonResult(response);
            }

            WorkoutService.ArchiveWorkout(workoutId);

            response.Success = true;

            return new JsonResult(response);
        }

        public async Task<JsonResult> OnPutUpdateWorkout(WorkoutDto updatedWorkout)
        {
            var response = new BaseResponse()
            {
                Success = false
            };

            var userId = (await UserService.GetCurrentUser(HttpContext)).Id;
            var rawWorkout = WorkoutService.GetWorkoutById((int)updatedWorkout.Id);

            if (rawWorkout == null || rawWorkout.UserId != userId)
            {
                response.Message = "Unable to update";
                return new JsonResult(response);
            }

            string validationErrors = WorkoutService.ValidateWorkout(updatedWorkout, false);

            if (!string.IsNullOrEmpty(validationErrors))
            {
                response.Message = validationErrors;
                return new JsonResult(response);
            }

            var settings = new WorkoutSettings()
            {
                AudioEndExercise = updatedWorkout.AudioEndExercise,
                AudioSecondTick = updatedWorkout.AudioSecondTick,
                AudioSpeakExercise = updatedWorkout.AudioSpeakExercise,
                AudioSpeakGo = updatedWorkout.AudioSpeakGo,
                TransitionTime = updatedWorkout.TransitionTime
            };

            var workout = new Workout()
            {
                Id = updatedWorkout.Id,
                Title = updatedWorkout.Title,
                Active = true,
                UserId = userId,
                ExercisesJson = JsonConvert.SerializeObject(updatedWorkout.Exercises),
                SettingsJson = JsonConvert.SerializeObject(settings)
            };

            WorkoutService.UpdateWorkout(workout);

            response.Success = true;

            return new JsonResult(response);
        }

    }
}
