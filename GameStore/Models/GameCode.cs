using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Models
{
    public class GameCode
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Code { get; set; }
    }
}