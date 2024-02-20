using SmartGarage.Exceptions;
using SmartGarage.Models;
using SmartGarage.Services;

namespace SmartGarage.Helpers
{
    public class Authenticator
    {

        private readonly IUserService userService;

        public Authenticator(IUserService userService)
        {
            this.userService = userService;
        }

        public virtual User AuthenticateUser(string username, string password)
        {

            try
            {
                var user = userService.Authenticate(username);

                if (!userService.VerifyPasswordHash(password, user))
                    throw new Exception("Invalid Password");

                return user;
            }
            catch (EntityNotFoundException)
            {
                throw new AuthorizationException("Invalid input");
            }
        }
    }
}
