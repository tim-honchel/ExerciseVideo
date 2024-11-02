using ExerciseVideo.Data.Entities;
using ExerciseVideo.Data.Repositories;
using ExerciseVideo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Components.Authorization;

namespace ExerciseVideo.Services
{
    public class UserService
    {
        UserRepository Repository;
        public UserService(UserRepository repository)
        {
            Repository = repository;
        }
        public async Task<User> GetCurrentUser(HttpContext context)
        {
            var nameIdentifier = context.User.Claims.Where(c => c.Type.Equals(System.Security.Claims.ClaimTypes.NameIdentifier))
                .Select(c => c.Value)
                .FirstOrDefault();
            var user = await Repository.GetOrCreateUser(nameIdentifier ?? string.Empty);
            return user;
        }

        public async Task<User> GetCurrentUser(AuthenticationState state)
        {
            var nameIdentifier = state.User.Claims
              .Where(c => c.Type.Equals(System.Security.Claims.ClaimTypes.NameIdentifier))
              .Select(c => c.Value)
              .FirstOrDefault();
            var user = await Repository.GetOrCreateUser(nameIdentifier ?? string.Empty);
            return user;
        }
    }
}
