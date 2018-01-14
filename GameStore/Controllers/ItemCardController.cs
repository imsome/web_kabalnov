using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class ItemCardController : Controller
    {
        // GET: ItemCard
        public ActionResult Card(int id)
        {
            return View(id);
        }
        public ActionResult Success()
        {
            return View();
        }
    }
}