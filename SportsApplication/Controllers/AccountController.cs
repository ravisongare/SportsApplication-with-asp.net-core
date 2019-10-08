using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsApplication.Models;
using SportsApplication.Models.Repository;

namespace SportsApplication.Controllers
{
    public class AccountController : Controller
    {
      
        private readonly IUnitOfWork unitOfWork;

        public AccountController( IUnitOfWork unitOfWork)
        {          
           
            this.unitOfWork = unitOfWork;
        }



        // GET: /<controller>/
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
        
            bool v =unitOfWork.data.IsSignedIn(User);
            if (v)
            {
                var current = unitOfWork.data.GetUserAsync(HttpContext.User).Result;
                var rolename = unitOfWork.data.getRole(current);
                if (rolename == "Athlete")
                    return RedirectToAction("Athlete", "Test");
                return RedirectToAction("Index", "Test");
            }
            return View();
        }

       

        //[AcceptVerbs("Get", "Post")]
        //public async Task<IActionResult> IsEmailInUse(string email)
        //{
        //    var user = await userManager.FindByEmailAsync(email);

        //    if (user == null)
        //    {
        //        return Json(true);
        //    }
        //    else
        //    {
        //        return Json($"Email {email} is already in use.");
        //    }
        //}
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };
                //var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                IdentityResult result = await unitOfWork.data.RegisterUser(model, user);
                

                //var identityRole = new IdentityRole()
                //{
                //    Name = model.Role
                //};

               // await unitOfWork.data. CreateRole(identityRole);

                if (result.Succeeded)
                {
                    await unitOfWork.data.AddRole(model, user);
                    await unitOfWork.data.SignIn(user);
                    
                    return RedirectToAction("Index", "Test");
                }
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

            }
            return View(model);
        }

      






        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (unitOfWork.data.IsSignedIn(User))
            {
                var current = unitOfWork.data.GetUserAsync(HttpContext.User).Result;
                var rolename = unitOfWork.data.getRole(current);
                if (rolename == "Athlete")
                    return RedirectToAction("Athlete", "Test");
                return RedirectToAction("Index", "Test");
            }
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await unitOfWork.data. Login(model);
                //if (!string.IsNullOrEmpty(returnUrl) &&Url.IsLocalUrl(returnUrl))
                //{
                //    return Redirect(returnUrl);
                //}
                //else
                if (result.Succeeded)
                    return RedirectToAction("Index", "Test");


            }
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            return View(model);
        }

       
        public async Task<IActionResult> Logout()
        {
            await unitOfWork.data. Signout();
            return RedirectToAction("Index", "Home");
        }

       
    }
}
