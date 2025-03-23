using MusiCodeWebApp.Filters;
using MusiCodeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusiCodeWebApp.Controllers
{
    public class HomeController : Controller
    {
        MusiCodeDBModel db = new MusiCodeDBModel();
        public ActionResult Index()
        {
            return View(db.Products.Where(x => x.IsDeleted == false && x.IsActive == true));
        }
        [MemberLoginRequiredFilter]
        public ActionResult _GetCartCount()
        {
            int mid = (Session["member"] as Member).ID;
            int count = db.Carts.Count(x => x.Member_ID == mid);
            ViewBag.count = count;
            return View();
        }
    }
}