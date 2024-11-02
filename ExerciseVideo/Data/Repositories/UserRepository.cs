using ExerciseVideo.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExerciseVideo.Data.Repositories
{
    public class UserRepository
    {
        public Context Context { get; set; }
        
        public UserRepository(Context context)
        {
            Context = context;
        }
        
        public async Task<User> GetOrCreateUser(string nameIdentifierClaim)
        {
            User? user = await GetUser(nameIdentifierClaim);
            return user ?? await CreateUser(nameIdentifierClaim);
        }

        private async Task<User?> GetUser(string nameIdentifierClaim)
        {
            return await Context.User.SingleOrDefaultAsync(u => u.NameIdentifierClaim == nameIdentifierClaim);
        }

        private async Task<User> CreateUser(string nameIdentifierClaim)
        {
            User newUser = new User { NameIdentifierClaim = nameIdentifierClaim };
            Context.User.Add(newUser);
            Context.SaveChanges();
            return await GetUser(nameIdentifierClaim);
        }
    }
}
