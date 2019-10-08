using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsApplication.Models;
using SportsApplication.Models.Repository;

namespace SportsApplication.Controllers
{
    public class AthleteController : Controller
    {

        private readonly IUnitOfWork unitOfWork;

        public AthleteController(IUnitOfWork unitOfWork)
        {

            this.unitOfWork = unitOfWork;
        }

        //Get: Test/AddAthlte/6
        [Authorize(Roles = "Admin")]
        public IActionResult AddAthlete(string id)
        {

            AddAthlete addAthlete = new AddAthlete();
            addAthlete.result = new Result();
            addAthlete.result.test_id = id;
            addAthlete.athlete = unitOfWork.data.GetAllAthlete();
            return View(addAthlete);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddAthlete(Result result)
        {
           // var currentUser = HttpContext.User;
            var tmp1 = unitOfWork.data.GetResultsById(result.test_id);
            foreach (var tmp in tmp1)
            {
                if (result.user_id == tmp.user_id)
                {
                    ViewBag.Message = "This Athlete Already Exist";
                    AddAthlete addAthlete = new AddAthlete();
                    addAthlete.result = result;
                    addAthlete.athlete = unitOfWork.data.GetAllAthlete();
                    return View(addAthlete);

                }
            }
            if (result.distance <= 1000)
                result.Fitness = "Below Average";
            if (result.distance > 1000 && result.distance <= 2000)
                result.Fitness = "Average";
            if (result.distance > 2000 && result.distance <= 3500)
                result.Fitness = "Good";
            if (result.distance > 3500)
                result.Fitness = "Very Good";

            unitOfWork.data.AddResult(result);
            unitOfWork.commit();
            unitOfWork.data.AddParticipant(result.test_id);
            unitOfWork.commit();
            return RedirectToAction("Details", "Test", new { id = result.test_id });
        }

    }
}