﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SportsApplication.Models
{
    public class Test
    {
        [Key]
        public string id { get; set; }
        [Display(Name ="Type")]
        public string type { get; set; }
        [Display(Name = "Date")]
        public int date { get; set; }
        [Display(Name = "Participants")]
        public int count { get; set; } = 0;
        public string User_id { get; set; }
    }
}
