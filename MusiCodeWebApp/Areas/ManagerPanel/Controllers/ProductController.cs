using MusiCodeWebApp.Areas.ManagerPanel.Filters;
using MusiCodeWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusiCodeWebApp.Areas.ManagerPanel.Controllers
{
    [ManagerLoginRequiredFilter]
    public class ProductController : Controller
    {
        MusiCodeDBModel db = new MusiCodeDBModel();
        public ActionResult Index()
        {
            return View(db.Products.Where(x=> x.IsDeleted == false).ToList());
        }

      
        public ActionResult Details(int id)
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Category_ID = new SelectList(db.Categories.Where(x => x.IsDeleted == false), "ID", "Name");
            ViewBag.Brand_ID = new SelectList(db.Brands.Where(x => x.IsDeleted == false), "ID", "Name");
            return View();
        }

        
        [HttpPost]
        public ActionResult Create(Product Model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isvalidimage = true;
                    if (image != null)
                    {
                        FileInfo fi = new FileInfo(image.FileName);
                        string extension = fi.Extension;
                        if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                        {
                            string name = Guid.NewGuid().ToString() + extension;
                            Model.Image = name;
                            image.SaveAs(Server.MapPath("~/Assets/ProductImages/" + name));
                        }
                        else
                        {
                            isvalidimage = false;
                            ViewBag.mesaj = "Resim uzantısı .jpg, .jpeg, .png olabilir";
                        }
                    }
                    else
                    {
                        Model.Image = "none.jpg";
                    }
                    if (isvalidimage)
                    {
                        Model.CreationTime = DateTime.Now;
                        db.Products.Add(Model);
                        db.SaveChanges();
                        TempData["mesaj"] = "Ürün ekleme başarılı";
                        return RedirectToAction("Index", "Product");
                    }
                }
                catch
                {
                    ViewBag.mesaj = "Ürün eklenirken bir hata oluştu";
                }
            }
            ViewBag.Category_ID = new SelectList(db.Categories.Where(x => x.IsDeleted == false), "ID", "Name");
            ViewBag.Brand_ID = new SelectList(db.Brands.Where(x => x.IsDeleted == false), "ID", "Name");
            return View(Model);
        }

        // GET: ManagerPanel/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id != null)
            {
                Product p = db.Products.Find(id);
                if (p != null)
                {
                    if (!p.IsDeleted)
                    {
                        ViewBag.Category_ID = new SelectList(db.Categories.Where(x => x.IsDeleted == false), "ID", "Name",p.Category_ID);
                        ViewBag.Brand_ID = new SelectList(db.Brands.Where(x => x.IsDeleted == false), "ID", "Name", p.Brand_ID);
                        return View(p);
                    }
                    else
                    {
                        TempData["systemerror"] = "Ürün silinmiş";
                        return RedirectToAction("Error", "System");
                    }
                }
                else
                {
                    TempData["systemerror"] = "Ürün Bulunamadı";
                    return RedirectToAction("Error", "System");
                }
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }

        // POST: ManagerPanel/Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product Model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool isvalidimage = true;
                    if (image != null)
                    {
                        FileInfo fi = new FileInfo(image.FileName);
                        string extension = fi.Extension;
                        if (extension == ".jpg" || extension == ".jpeg" || extension == ".png")
                        {
                            string name = Guid.NewGuid().ToString() + extension;
                            Model.Image = name;
                            image.SaveAs(Server.MapPath("~/Assets/ProductImages/" + name));
                        }
                        else
                        {
                            isvalidimage = false;
                            ViewBag.mesaj = "Resim uzantısı .jpg, .jpeg, .png olabilir";
                        }
                    }
                    if (isvalidimage)
                    {
                        db.Entry(Model).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                        TempData["mesaj"] = "Ürün Düzenleme başarılı";
                        return RedirectToAction("Index", "Product");
                    }
                }
                catch
                {
                    ViewBag.mesaj = "Ürün düzenlenirken bir hata oluştu";
                }
            }
            return View(Model);
          
        }
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                Product p = db.Products.Find(id);
                if (p != null)
                {
                    p.IsDeleted = true;
                    p.IsActive = false;
                    p.IsRecent = false;
                    db.SaveChanges();
                    TempData["mesaj"] = "Ürün silindi";
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    TempData["systemerror"] = "Ürün Bulunamadı";
                    return RedirectToAction("Error", "System");
                }
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }
        public ActionResult Test()
        {
            return View();
        }
        [HttpGet]
        public ActionResult _Create()
        {
            ViewBag.Category_ID = new SelectList(db.Categories.Where(x => x.IsDeleted == false), "ID", "Name");
            ViewBag.Brand_ID = new SelectList(db.Brands.Where(x => x.IsDeleted == false), "ID", "Name");
            return View();
        }
    }
}
