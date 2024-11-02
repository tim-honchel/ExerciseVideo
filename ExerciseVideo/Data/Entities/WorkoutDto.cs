﻿using Microsoft.AspNetCore.Mvc;

namespace ExerciseVideo.Data.Entities
{
    public class WorkoutDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public List<Exercise> Exercises { get; set; } = new();
        public bool AudioEndExercise { get; set; }
        public bool AudioSecondTick { get; set; }
        public bool AudioSpeakExercise { get; set; }
        public bool AudioSpeakGo { get; set; }
        public int TransitionTime { get; set; }
    }
}
