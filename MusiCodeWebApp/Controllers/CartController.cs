using MusiCodeWebApp.Filters;
using MusiCodeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusiCodeWebApp.Controllers
{
    [MemberLoginRequiredFilter]
    public class CartController : Controller
    {
        MusiCodeDBModel db = new MusiCodeDBModel();
        public ActionResult Index()
        {
            int mid = (Session["member"] as Member).ID;
            return View(db.Carts.Where(x=> x.Member_ID == mid).ToList());
        }

        public ActionResult Add(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }
            Product p = db.Products.Find(id);
            if (p == null)
            {
                return RedirectToAction("Index", "Home");
            }
            int mid = (Session["member"] as Member).ID;
            int count = db.Carts.Count(x => x.Member_ID == mid && x.Product_ID == id);
            if (count == 0)
            {
                Cart c = new Cart();
                c.Product_ID = Convert.ToInt32(id);
                c.Member_ID = mid;
                c.Quantity = 1;
                db.Carts.Add(c);
                db.SaveChanges();
                Session["cartcount"] = Convert.ToInt32(Session["cartcount"]) + 1;
            }
            else
            {
                int cid = db.Carts.FirstOrDefault(x => x.Member_ID == mid && x.Product_ID == id).ID;
                Cart c = db.Carts.Find(cid);
                c.Quantity = c.Quantity + 1;
                db.SaveChanges();
            }
            return RedirectToAction("Index", "Cart");
        }

        public ActionResult Increase(int? id)
        {

            return View();
        }

        public ActionResult Decrease(int? id)
        {

            return View();
        }

        public ActionResult Remove(int? id)
        {

            return View();
        }
    }
}