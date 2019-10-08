using Microsoft.AspNetCore.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SportsApplication.Models
{
    public interface IData
    {
       void AddResult(Result result);

       void DeleteTest(Test test);

       void Addtest(Test test);
      
        void UpdateAthlete(Athlete athlete);
      
        void AddParticipant(string id);
        
        Athlete GetAthlete(string id);

       Test GetTestByid(string id);
     
        IQueryable<Detail> GetDeatail(string id);

       IEnumerable<Test>GetAllTestById(string id);

        List<IdentityUser> GetAllAthlete();

       Result getResult(string testId, string UserId);

        IQueryable<AthlteDetail> getAthleteDetail(string id);

        IEnumerable<Result> GetResultsById(string id);

        Result getResultById(int id);

        void DeleteResult(Result result);

        void UpdateResult(Result result);

        void RemoveParticipant(string id);

        Task<IdentityUser> GetUserAsync(ClaimsPrincipal httpuser);

        Task<IdentityResult> RegisterUser(RegisterViewModel model, IdentityUser user);

        Task AddRole(RegisterViewModel model, IdentityUser user);

        bool IsSignedIn(ClaimsPrincipal User);

        string getRole(IdentityUser current);

        Task SignIn(IdentityUser user);

        Task<SignInResult> Login(LoginViewModel model);

        Task Signout();

        Task<IdentityResult> CreateRole(IdentityRole identityRole);

       //IdentityUser CurrentUser();
    }
}
