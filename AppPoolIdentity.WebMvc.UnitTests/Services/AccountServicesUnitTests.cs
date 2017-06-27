using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AppPoolIdentity.WebMvc.UnitTests.Attributes;
using AppPoolIdentity.WebMvc.Common;
using AppPoolIdentity.WebMvc.Services;
using AppPoolIdentity.WebMvc.Entities;

namespace AppPoolIdentity.WebMvc.UnitTests.Services
{
    [TestClass]
    public class AccountServicesUnitTests
    {
        [TestClass]
        public class CreateAccount
        {
            [TestMethod]
            [ExpectedExceptionWithMessage(typeof(ServiceException),"User Name is null or empty.")]
            public void CreateAccount_UserNameIsNullOrEmpty_ThrowException()
            {
                var service = new AccountService();

                bool result = service.CreateAccount(new User());
            }

            [TestMethod]
            [ExpectedExceptionWithMessage(typeof(ServiceException), "Email is null or empty.")]
            public void CreateAccount_EmailIsNullOrEmpty_ThrowException()
            {
                var service = new AccountService();
                var user = new User();
                user.UserName = "vvilelaj";

                bool result = service.CreateAccount(user);
            }

            [TestMethod]
            [ExpectedExceptionWithMessage(typeof(ServiceException), "Password is null or empty.")]
            public void CreateAccount_PasswordIsNullOrEmpty_ThrowException()
            {
                var service = new AccountService();
                var user = new User();
                user.UserName = "vvilelaj";
                user.Email = "victor.vilela@outlook.com";
                user.Password = "";

                bool result = service.CreateAccount(user);
            }

            [TestMethod]
            [ExpectedExceptionWithMessage(typeof(ServiceException), "Confirm Password is null or empty.")]
            public void CreateAccount_ConfirmPasswordIsNullOrEmpty_ThrowException()
            {
                var service = new AccountService();
                var user = new User();
                user.UserName = "vvilelaj";
                user.Email = "victor.vilela@outlook.com";
                user.Password = "123";
                user.ConfirmPassword = "";

                bool result = service.CreateAccount(user);
            }

            [TestMethod]
            [ExpectedExceptionWithMessage(typeof(ServiceException), "Password and Confirm Password are diferent.")]
            public void CreateAccount_PasswordAndConfirmPasswordIsNullOrEmpty_ThrowException()
            {
                var service = new AccountService();
                var user = new User();
                user.UserName = "vvilelaj";
                user.Email = "victor.vilela@outlook.com";
                user.Password = "123";
                user.ConfirmPassword = "321";

                bool result = service.CreateAccount(user);
            }
        }
    }
}
