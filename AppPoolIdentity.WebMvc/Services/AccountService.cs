using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;

using AppPoolIdentity.WebMvc.Common;
using AppPoolIdentity.WebMvc.Entities;
using AppPoolIdentity.WebMvc.Models;

using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Security.Claims;

using Microsoft.Owin.Security;

namespace AppPoolIdentity.WebMvc.Services
{
    public class AccountService
    {
        public bool CreateAccount(User user)
        {
            if (user == null) throw new ArgumentNullException("user");

            if (string.IsNullOrWhiteSpace(user.UserName)) throw new ServiceException("User Name is null or empty.");

            if (string.IsNullOrWhiteSpace(user.Email)) throw new ServiceException("Email is null or empty.");

            if (string.IsNullOrWhiteSpace(user.Password)) throw new ServiceException("Password is null or empty.");

            if (string.IsNullOrWhiteSpace(user.ConfirmPassword)) throw new ServiceException("Confirm Password is null or empty.");

            if (user.Password != user.ConfirmPassword) throw new ServiceException("Password and Confirm Password are diferent.");

            var result = GetUserManager().Create(user, user.Password);

            return result.Succeeded;
        }

        public bool LogIn(User user)
        {
            var singInManager = new SignInManager<User, string>(GetUserManager(), HttpContext.Current.GetOwinContext().Authentication);
            var result = singInManager.PasswordSignIn(user.UserName, user.Password, false, false);
            return result == SignInStatus.Success;
        }

        public void LogOut()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        private  UserManager<User> GetUserManager()
        {
            var dbContext = new IdentityDbContext<User>("DefaultConnection");
            var userStore = new UserStore<User>(dbContext);
            var userManager = new UserManager<User>(userStore);
            return userManager;
        }
    }
}