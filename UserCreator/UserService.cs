using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using UserCreator.Model;

namespace UserCreator
{
    public class UserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task CreateUserIfNotExist(User user, string password)
        {
            var existingUser = await _userManager.FindByNameAsync(user.UserName);
            if (existingUser == null)
            {
                var result = await _userManager.CreateAsync(user, password);

                if (!result.Succeeded)
                {
                    var message = string.Empty;
                    result.Errors.ToList().ForEach(x => message += x.Description + ';');
                    throw new ArgumentException(message);
                }
            }

        }
    }
}
