using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsApplication.Models;
using SportsApplication.Models.Repository;

namespace SportsApplication.Controllers
{
    public class AdministrationController : Controller
    {
       
        private readonly IUnitOfWork unitOfWork;

        public AdministrationController(IUnitOfWork unitOfWork)
        {
         
            this.unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        public async Task<IActionResult> CreateRole(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var identityRole = new IdentityRole()
                {
                    Name = model.RoleName
                };

                IdentityResult result = await unitOfWork.data.CreateRole(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Test");
                }
                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }


            return View(model);

        }

       
    }
}