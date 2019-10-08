using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    public class TestController : Controller
    {
        public IEnumerable<SelectListItem> Athlete1 { get; set; }

       
        private readonly IUnitOfWork unitOfWork;
    
        
        public TestController(IUnitOfWork unitOfWork)
        {
          
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Athlete()
        {
            var id = unitOfWork.data.GetUserAsync(HttpContext.User).Result.Id;
         
            var result =unitOfWork.data.getAthleteDetail(id);
            return View(result);
        }
        [HttpGet]
        public IActionResult AthleteTestDetail(int id)
        {
          
            var result = unitOfWork.data.getResultById(id);
            return View(result);
        }



        // GET: Test
        public IActionResult Index()
        {
            IdentityUser current = unitOfWork.data.GetUserAsync(HttpContext.User).Result;
            string rolename =unitOfWork.data.getRole(current);
            if (rolename == "Athlete")
                return RedirectToAction("Athlete", "Test");
            return View(unitOfWork.data.GetAllTestById(current.Id));
        }


        // GET: Test/Details/5
        public IActionResult Details(string id)
        {
            
            var tmp = unitOfWork.data.GetTestByid(id);
            //(from r in _context.Tests
            //          where r.id == id
            //          select r).FirstOrDefault();

            if (tmp == null)
                return NotFound();
            ViewData["TestType"] = tmp.type;
            ViewData["Date"] = tmp.date;
            ViewData["id"] = id;
            IQueryable<Detail> test = unitOfWork.data.GetDeatail(id);
            var t = test.ToList();

            if (test == null)
            {
                return NotFound();
            }

            return View(t);
        }

        // GET: Test/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {

            Test test = new Test();
            test.User_id = unitOfWork.data.GetUserAsync(HttpContext.User).Result.Id;
            return View(test);
        }

        // POST: Test/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(Test test)
        {
               unitOfWork.data.Addtest(test);
            unitOfWork.commit();
                return RedirectToAction(nameof(Index));
        }


        // GET: Test/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            

            var test = unitOfWork.data.GetTestByid(id); 
            if (test == null)
            {
                return NotFound();
            }

            return View(test);
        }

        // POST: Test/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(string id)
        {
            var test = unitOfWork.data.GetTestByid(id);
            unitOfWork.data.DeleteTest(test);
            unitOfWork.commit();
             var results =unitOfWork.data.GetResultsById(id);
            if (results != null)
                foreach (var temp in results)
                {
                    unitOfWork.data.DeleteResult(temp);
                  
                }
            unitOfWork.commit();
            return RedirectToAction("Index");
        }

    }
}
