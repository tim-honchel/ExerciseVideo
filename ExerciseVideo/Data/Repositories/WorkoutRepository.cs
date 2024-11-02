using ExerciseVideo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExerciseVideo.Data.Repositories
{
    public class WorkoutRepository
    {
        public Context Context { get; set; }

        public WorkoutRepository(Context context)
        {
            Context = context;
        }

        public List<Workout> GetWorkoutsByUserId(int userId)
        {
            var allWorkouts = Context.Workout.ToList();
            return allWorkouts.Where(w => w.UserId == userId).ToList();
        }

        public void AddWorkout(Workout workout)
        {
            Context.Workout.Add(workout);
            Context.SaveChanges();

        }

    }
}
