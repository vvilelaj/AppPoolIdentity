using AppPoolIdentity.WebMvc.Common;
using AppPoolIdentity.WebMvc.Entities;
using AppPoolIdentity.WebMvc.Models;
using AppPoolIdentity.WebMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPoolIdentity.WebMvc.Controllers
{
    public class AccountController : Controller
    {
        private AccountService accountService;

        public AccountController()
        {
            accountService = new AccountService();
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Create(CreateAccountModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new
                    {
                        ok = false,
                        Message = ModelState.Values.SelectMany(v => v.Errors).FirstOrDefault().ErrorMessage
                    }, JsonRequestBehavior.AllowGet);
                }

                var user = new User()
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    ConfirmPassword = model.ConfirmPassword
                };

                var result = accountService.CreateAccount(user);
                if (result)
                {
                    return Json(new
                    {
                        ok = result,
                        Message = "Account created succesfully."
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new
                    {
                        ok = result,
                        Message = "Can not create the Account."
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (ServiceException se)
            {
                return Json(new
                {
                    ok = false,
                    Message = se.Message
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception se)
            {
                return Json(new
                {
                    ok = false,
                    Message = "Error"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LogInModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = new User() { UserName = model.UserName, Password = model.Password };
            if (accountService.LogIn(user))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Mesage = "User Name or Password incorrect.";
            return View(model);
        }

        public ActionResult LogOut()
        {
            accountService.LogOut();
            return RedirectToAction("Index", "Home");
        }
    }
}