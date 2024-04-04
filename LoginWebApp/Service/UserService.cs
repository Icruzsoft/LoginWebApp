using LoginWebApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace LoginWebApp.Services
{
    public class UserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticationManager _authenticationManager;
        private readonly ApplicationDbContext _dbContext; 

        public UserService()
        {
            _userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            _authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            _dbContext = new ApplicationDbContext(); 
        }

        public ApplicationUser ValidateUser(string email, string password)
        {
            var user = _userManager.FindByEmail(email);
            if (user != null && _userManager.CheckPassword(user, password))
            {
                return user;
            }
            return null;
        }

        public async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            _authenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        public void SignOut()
        {
            _authenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

      
        public ApplicationUser FindByEmail(string email)
        {
            return _userManager.FindByEmail(email);
        }

        public IdentityResult RegisterClient(ApplicationUser user, string password)
        {
            var result = _userManager.Create(user, password);
            if (result.Succeeded)
            {
                _userManager.AddToRole(user.Id, "Client");
            }
            return result;
        }

        public IdentityResult RegisterExpert(ApplicationUser user, string password)
        {
            var result = _userManager.Create(user, password);
            if (result.Succeeded)
            {
                _userManager.AddToRole(user.Id, "Expert");
            }
            return result;
        }

        public ApplicationUser FindByToken(string token)
        {
           
            return _dbContext.Users.FirstOrDefault(u => u.SecurityStamp == token);
        }

        public bool ConfirmEmail(ApplicationUser user)
        {
            user.EmailConfirmed = true;
            var result = _userManager.Update(user);
            return result.Succeeded;
        }

        public IdentityResult UpdateUser(ApplicationUser user)
        {
            return _userManager.Update(user);
        }

        
    }
}

