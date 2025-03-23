using MusiCodeWebApp.Models;
using MusiCodeWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusiCodeWebApp.Controllers
{
    public class MemberController : Controller
    {

        MusiCodeDBModel db = new MusiCodeDBModel();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Member model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int count = db.Members.Count(x => x.Mail == model.Mail);
                    if (count == 0)
                    {
                        model.CreationTime = DateTime.Now;
                        model.IsActive = true;
                        db.Members.Add(model);
                        db.SaveChanges();
                        ViewBag.basarili = "Üyelik işlemi Başarılı";
                    }
                    else
                    {
                        ViewBag.basarisiz = "Bu mail daha önceden kayıt edilmiş";
                    }
                }
                catch
                {
                    ViewBag.basarisiz = "Üyelik işlemi Başarısız";
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(MemberLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Member m = db.Members.FirstOrDefault(x => x.Mail == model.Mail && x.Password == model.Password);
                    if (m != null)
                    {
                        if (m.IsActive)
                        {
                            Session["member"] = m;
                            Session["cartcount"] = db.Carts.Count(x => x.Member_ID == m.ID);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ViewBag.basarisiz = "Hesabınız askıya alınmıştır";
                        }
                    }
                    else
                    {
                        ViewBag.basarisiz = "Kullanıcı bulunamadı";
                    }
                }
                catch
                {
                    ViewBag.basarisiz = "Bir Hata Oluştu";
                }
            }
            return View(model);
        }
    }
}