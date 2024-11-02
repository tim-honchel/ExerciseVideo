using ExerciseVideo.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics.Eventing.Reader;
using System.Xml.Linq;

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
        public IActionResult OnGet(int? id)
        {
            WorkoutDto workout = new WorkoutDto();
            
            if (id == null || id < -1)
            {
                return Redirect("Error");
            }
            // sample workout
            else if (id == -1)
            {
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
            }
            else
            {
                // TODO
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
