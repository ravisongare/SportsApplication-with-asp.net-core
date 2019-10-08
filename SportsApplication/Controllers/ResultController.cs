using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportsApplication.Models;
using SportsApplication.Models.Repository;

namespace SportsApplication.Controllers
{
    public class ResultController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ResultController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        // GET: Result/Edit/5
        public IActionResult Edit(int id)
        {
            EditAthleteResult tmp = new EditAthleteResult();
            tmp.athletes = unitOfWork.data.GetAllAthlete();
            tmp.result =unitOfWork.data.getResultById(id);


            if (tmp== null)
            {
                return NotFound();
            }
            return View(tmp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string Name, [Bind("id,user_id,test_id,distance,Fitness")] Result result)
        {
            var tmp = unitOfWork.data.GetResultsById(result.test_id);
            foreach (var temp in tmp)
            {
                if (result.user_id == temp.user_id && result.id!=temp.id)
                {

                    ViewBag.Message = "Athlete already exist";
                    EditAthleteResult tmp1 = new EditAthleteResult();
                    tmp1.athletes = unitOfWork.data.GetAllAthlete();
                    tmp1.result = result;
                    return View(tmp1);
                }
            }
            if (ModelState.IsValid)
            {
                
                    //var temp = data.GetAthlete(result.user_id);
                  //  temp.name = Name;
                   // data.UpdateAthlete(temp);
                   // data.commit();
                    unitOfWork.data.UpdateResult(result);
                     unitOfWork.commit();
                    return RedirectToAction("Details","Test", new{id=result.test_id});
            }
            return View(result);
        }

        // GET: Result/Delete/5
        public IActionResult Delete(int id)
        {
            var result =unitOfWork.data.getResultById(id);
           
     
            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }

        // POST: Result/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = unitOfWork.data.getResultById(id);
            unitOfWork.data.DeleteResult(result);
            unitOfWork.data.RemoveParticipant(result.test_id);
            unitOfWork.commit();
            
            return RedirectToAction("Details","Test", new{ id=result.test_id});
        }
    }
}
