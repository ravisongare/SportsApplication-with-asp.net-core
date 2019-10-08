using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplication.Models
{
    public class AddAthlete
    {
        public Result result { get; set; }
        public List<IdentityUser> athlete { get; set; }
    }
}
