using System;
using UserCreator.Model;

namespace UserCreator
{
    class Program
    {
        static void Main()
        {
            var connectionString = string.Empty;
            Console.WriteLine("Enter the connection string: ");
            connectionString = Console.ReadLine();

            Console.WriteLine("Initialize database and services");

            var servicesProvider = ServiceProviderFactory.BuildServiceProvider(connectionString);
            Console.WriteLine("Services created");

            var userService = (UserService)servicesProvider.GetService(typeof(UserService));

            var user = new User();

            Console.WriteLine("Enter user name (only alphanumeric):");
            user.UserName = Console.ReadLine();

            Console.WriteLine("Enter email (email@url.com):");
            user.Email = Console.ReadLine();
            while(!Validator.IsValidEmail(user.Email))
            {
                Console.WriteLine("Email is not valid, please enter correct email:");
                user.Email = Console.ReadLine();
            }
            Console.WriteLine("Enter password (must include a-z, A-Z and 0-9, min length - 8):");
            var password = Console.ReadLine();
            try
            {
                userService.CreateUserIfNotExist(user, password).Wait();
                Console.WriteLine("User created succesfully");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
       
    }
}
