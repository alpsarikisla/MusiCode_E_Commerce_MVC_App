using MusiCodeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusiCodeWebApp.Filters;

namespace MusiCodeWebApp.Controllers
{
    [MemberLoginRequiredFilter]
    public class FavoriteController : Controller
    {
        MusiCodeDBModel db = new MusiCodeDBModel();
        public ActionResult Index()
        {
            Member m = Session["member"] as Member;
            return View(db.Favorites.Where(x=> x.Member_ID == m.ID).ToList());
        }
        public ActionResult Add(int? id)
        {
            if (Session["member"] != null)
            {
                if (id != null)
                {
                    int count = db.Products.Count(x => x.ID == id);
                    if (count > 0)
                    {
                        int mid = (Session["member"] as Member).ID;
                        int c2 = db.Favorites.Count(x => x.Member_ID == mid && x.Product_ID == id);
                        if (c2 == 0)
                        {
                            Favorite f = new Favorite();
                            f.Member_ID = (Session["member"] as Member).ID;
                            f.Product_ID = Convert.ToInt32(id);
                            db.Favorites.Add(f);
                            db.SaveChanges();
                            TempData["info"] = "Favorilere Eklendi";
                        }
                        else
                        {
                            TempData["info"] = "Favorilerinize Zaten Ekli";
                        }

                    }
                }
            }
            else
            {
                TempData["info"] = "Favorilere Eklemek İçin Giriş Yapınız";
                return RedirectToAction("Login", "Member");
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Remove(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Favorite");
            }

            Favorite favorite= db.Favorites.Find(id);
            db.Favorites.Remove(favorite);
            db.SaveChanges();
            TempData["info"] = "Favorilerinizden çıkarıldı";
            return RedirectToAction("Index", "Favorite");
        }
    }
}