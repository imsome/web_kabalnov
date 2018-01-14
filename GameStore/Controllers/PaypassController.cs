using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class PaypassController : Controller
    {
        // GET: Paypass
        public ActionResult Paypass(int id)
        {
            return View(id);
        }

    }
}