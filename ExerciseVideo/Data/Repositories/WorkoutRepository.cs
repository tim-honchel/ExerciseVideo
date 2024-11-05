using ExerciseVideo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExerciseVideo.Data.Repositories
{
    public class WorkoutRepository
    {
        public IDbContextFactory<Context> ContextFactory { get; set; }

        public WorkoutRepository(IDbContextFactory<Context> contextFactory)
        {
            ContextFactory = contextFactory;
        }

        public void AddWorkout(Workout workout)
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                context.Workout.Add(workout);
                context.SaveChanges();
            }
        }

        public Workout? GetWorkoutById(int workoutId)
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                return context.Workout.Where(w => w.Id == workoutId).SingleOrDefault();
            }
        }

        public List<Workout> GetWorkoutsByUserId(int userId)
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                var allWorkouts = context.Workout.ToList();
                return allWorkouts.Where(w => w.UserId == userId).ToList();
            }
        }

        public void UpdateWorkout(Workout workout)
        {
            using (var context = ContextFactory.CreateDbContext())
            {
                context.Workout.Update(workout);
                context.SaveChanges();
            }
        }
           

    }
}
