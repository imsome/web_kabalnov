using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class AdminMenuController : Controller
    {
        
        public ActionResult Menu()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddItem()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult AddCodes()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult GameCodesAdd(int id)
        {
            return View(id);
        }
        public ActionResult UserMenu()
        {
            return View();
        }
    }
}