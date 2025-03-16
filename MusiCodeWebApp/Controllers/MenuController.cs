using MusiCodeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusiCodeWebApp.Controllers
{
    public class MenuController : Controller
    {
        MusiCodeDBModel db = new MusiCodeDBModel();
        // GET: Menu
        public ActionResult _Index()
        {
            //List<Category> categories = db.Categories.ToList();
            //List<Category> notdeletedcategories = categories.Where(x => x.IsDeleted == false).ToList();
            return View(db.Categories.Where(x => x.IsDeleted == false).ToList());
        }
    }
}