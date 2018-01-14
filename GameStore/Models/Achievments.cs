using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Models
{
    public class Achievments
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstAchieveName { get; set; }                    //Купить игру
        public int FirstAchieveProgress { get; set; } 
        public string SecondAchieveName { get; set; }                   //Купить шутер
        public int SecondAchieveProgress { get; set; }
        public string ThirdAchieveName { get; set; }                    //Купить гонку
        public int ThirdAchieveProgress { get; set; }                   
        public string FourthAchieveName { get; set; }                   //Купить 10 игр
        public int FourthAchieveProgress { get; set; }                  
        public string FifthAchieveName { get; set; }                    //Купить 100 игр
        public int FifthAchieveProgress { get; set; }                   














    }
}