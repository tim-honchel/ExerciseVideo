using ExerciseVideo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Xml.Linq;

namespace ExerciseVideo.Pages
{
    public class WorkoutModel : PageModel
    {
        public bool AudioEndExercise;
        public bool AudioSecondTick;
        public bool AudioSpeakExercise;
        public bool AudioSpeakGo;
        public Exercise[]? Exercises;
        public int TransitionTime;
        public void OnGet()
        {
            Workout workout = new Workout()
            {
                Exercises = new List<Exercise>()
                {
                    new Exercise() {Name = "Arm Swings", Time = 20, Order = 0},
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
                },
                AudioEndExercise = true,
                AudioSecondTick = true,
                AudioSpeakExercise = true,
                AudioSpeakGo = true,
                TransitionTime = 2
            };

            Exercises = workout.Exercises.ToArray();
            AudioEndExercise = workout.AudioEndExercise;
            AudioSecondTick = workout.AudioSecondTick;
            AudioSpeakExercise = workout.AudioSpeakExercise;
            AudioSpeakGo = workout.AudioSpeakGo;
            TransitionTime = workout.TransitionTime;
        }
    }
}
