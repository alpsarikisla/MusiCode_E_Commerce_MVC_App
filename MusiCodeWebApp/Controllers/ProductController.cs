using MusiCodeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusiCodeWebApp.Controllers
{
    public class ProductController : Controller
    {
        MusiCodeDBModel db = new MusiCodeDBModel();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int? id)
        {
            if (id== null)
            {
                return RedirectToAction("Index", "Home");
            }
            Product p = db.Products.Find(id);
            if (p == null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(p);
        }
    }
}