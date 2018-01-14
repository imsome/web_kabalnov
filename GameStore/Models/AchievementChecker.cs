using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStore.Models
{
    public class AchievementChecker
    {
        [Key]   
        public string AchieveName { get; set; }
        public string UserName { get; set; }
        public double Percentage { get; set; }
    }
}