using MusiCodeWebApp.Areas.ManagerPanel.Filters;
using MusiCodeWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusiCodeWebApp.Areas.ManagerPanel.Controllers
{
    [ManagerLoginRequiredFilter]
    public class CategoryController : Controller
    {
        MusiCodeDBModel db = new MusiCodeDBModel();
        public ActionResult Index()
        {
            return View(db.Categories.Where(x=> x.IsDeleted == false).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpGet]
        public ActionResult _Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Categories.Add(model);
                    db.SaveChanges();
                    return RedirectToAction("Index", "Category");

                }
                catch
                {
                    ViewBag.mesaj = "Bir hata oluştu";
                }
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                Category c = db.Categories.Find(id);
                if (c != null)
                {
                    return View(c);
                }
            }
            return RedirectToAction("Index", "Category");
           
        }
        [HttpPost]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    TempData["mesaj"] = "Kategori güncelleme başarılı";
                    return RedirectToAction("Index", "Category");
                }
                catch
                {
                    ViewBag.mesaj = "Bir hata oluştu";
                }
            }
            return View(model);
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Category c = db.Categories.Find(id);
                if (c != null)
                {
                    c.IsDeleted = true;
                    c.IsActive = false;
                    db.SaveChanges();
                    TempData["mesaj"] = "Kategori silme işlemi başarılı";
                }
            }
            return RedirectToAction("Index", "Category");
        }
    }
}