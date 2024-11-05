using ExerciseVideo.Data.Entities;
using ExerciseVideo.Data.Repositories;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace ExerciseVideo.Services
{
    public class WorkoutService
    {
        public WorkoutRepository Repository { get; set; }
        public WorkoutService(WorkoutRepository repository)
        {
            Repository = repository;
        }
        public void AddWorkout(Workout workout)
        {
            Repository.AddWorkout(workout);
        }

        public void ArchiveWorkout(int workoutId)
        {
            var workout = GetWorkoutById(workoutId);
            workout.Active = false;
            UpdateWorkout(workout);
        }

        public WorkoutDto ConvertWorkoutToWorkoutDto(Workout workout)
        {
            var settings = JsonConvert.DeserializeObject<WorkoutSettings>(workout.SettingsJson) ?? new WorkoutSettings();

            var workoutDto = new WorkoutDto()
            {
                Id = workout.Id,
                Title = workout.Title,
                Exercises = JsonConvert.DeserializeObject<List<Exercise>>(workout.ExercisesJson) ?? new List<Exercise>(),
                AudioEndExercise = settings.AudioEndExercise,
                AudioSecondTick = settings.AudioSecondTick,
                AudioSpeakExercise = settings.AudioSpeakExercise,
                AudioSpeakGo = settings.AudioSpeakGo,
                TransitionTime = settings.TransitionTime
            };
            return workoutDto;
        }

        public WorkoutDto GetSampleWorkout()
        {
            var workout = new WorkoutDto();

            workout.Exercises = new List<Exercise>()
                {
                    new Exercise() {Name = "Arm Swings", Time = 10, Order = 0},
                    new Exercise() {Name = "Forward Arm Circles", Time = 10, Order = 1},
                    new Exercise() {Name = "Reverse Arm Circles", Time = 10, Order = 2},
                    new Exercise() {Name = "Right Leg Circles", Time = 10, Order = 3},
                    new Exercise() {Name = "Left Leg Circles", Time = 10, Order = 4},
                    new Exercise() {Name = "Right Leg Kicks", Time = 10, Order = 5},
                    new Exercise() {Name = "Left Leg Kicks", Time = 10, Order = 6},
                    new Exercise() {Name = "Deep Sit Bend", Time = 10, Order = 7},
                    new Exercise() {Name = "Push Ups", Time = 30, Order = 8},
                    new Exercise() {Name = "Sit Ups", Time = 30, Order = 9},
                    new Exercise() {Name = "Squats", Time = 30, Order = 10},
                    new Exercise() {Name = "Jumping Jacks", Time = 30, Order = 11},
                    new Exercise() {Name = "Deep Sit Bend", Time = 10, Order = 12},
                    new Exercise() {Name = "Push Ups", Time = 30, Order = 13},
                    new Exercise() {Name = "Sit Ups", Time = 30, Order = 14},
                    new Exercise() {Name = "Squats", Time = 30, Order = 15},
                    new Exercise() {Name = "Jumping Jacks", Time = 30, Order = 16}
                };
            workout.AudioEndExercise = true;
            workout.AudioSecondTick = true;
            workout.AudioSpeakExercise = true;
            workout.AudioSpeakGo = true;
            workout.TransitionTime = 2;

            return workout;
        }

        public List<Workout> GetWorkoutsByUserId(int userId)
        {
            return Repository.GetWorkoutsByUserId(userId);
        }

        public Workout? GetWorkoutById(int workoutId)
        {
            return Repository.GetWorkoutById(workoutId);
        }

        public void UpdateWorkout(Workout workout)
        {
            Repository.UpdateWorkout(workout);
        }

        public string ValidateWorkout(WorkoutDto newWorkout, bool isNew = true)
        {
            string validationErrorMessage = string.Empty;

            if (newWorkout == null)
            {
                validationErrorMessage = "Workout data is empty or was not received.";
                return validationErrorMessage;
            }

            if (isNew && newWorkout.Id != 0)
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
