using Microsoft.EntityFrameworkCore;
using User.Domain.Entities;

namespace User.Infrastructure.Context
{
    public class UserSeed
    {
        public static async Task SeedAsync(UserContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(GetSeedUsers());
                await context.SaveChangesAsync();
            }
        }

        private static IEnumerable<Domain.Entities.User> GetSeedUsers()
        {
            var listUsers = new List<Domain.Entities.User>();

            for (int i = 0; i < 5; i++)
            {
                var user = new Domain.Entities.User()
                {
                    FirstName = $"Test{i + 1}",
                    LastName = $"Test{i + 1}",
                    Email = $"test{i + 1}@mail.com",
                    Role = ((i + 1) > 3) ? ConstantsRole.Roles.Admin : ConstantsRole.Roles.Member,
                    UserName = $"test{i + 1}",
                    PasswordHash = "O6aEkS5O2/O71bAJ3TY+LgEyAwYSZYwLJd94QAt7zcA=",
                    PasswordSalt = "Vq4B9HqVaSjj6iJTRA0z8KMz6Z9IyGLN4xk9Jcl5WPQ3WWI3gM23Z+mQjHMTdhV9XDGh0wkdcqN6DirzMI/Wyg=="
                };

                listUsers.Add(user);
            }

            return listUsers;
        }
    }
}
