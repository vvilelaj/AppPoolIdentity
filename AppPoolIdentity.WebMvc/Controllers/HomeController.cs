﻿using AppPoolIdentity.WebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;

namespace AppPoolIdentity.WebMvc.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index(WebAppIdentityModel model)
        {
            model.AppPoolIdentityName = WindowsIdentity.GetCurrent().Name;
            model.UserIdentityName = !HttpContext.User.Identity.IsAuthenticated ?
                "(Anonymous)" : 
                HttpContext.User.Identity.Name;
            model.IsAuthenticated = HttpContext.User.Identity.IsAuthenticated;
            return View(model);
        }

        

        
    }
}