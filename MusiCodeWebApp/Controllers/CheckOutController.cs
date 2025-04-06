using MusiCodeWebApp.Filters;
using MusiCodeWebApp.Models;
using MusiCodeWebApp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;

namespace MusiCodeWebApp.Controllers
{
    [MemberLoginRequiredFilter]
    public class CheckOutController : Controller
    {
        MusiCodeDBModel db = new MusiCodeDBModel();
        public ActionResult PaymentSuccess()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Payment()
        {
            int mid = (Session["member"] as Member).ID;
            ViewBag.cart = db.Carts.Where(x => x.Member_ID == mid).ToList();
            return View();
           
        }
        [HttpPost]
        public ActionResult Payment(PaymentViewModel model)
        {
            int mid = (Session["member"] as Member).ID;
            List<Cart> memberCart = db.Carts.Where(x => x.Member_ID == mid).ToList();
            if (ModelState.IsValid)
            {
                decimal Total = memberCart.Sum(x => x.Product.Price * x.Quantity);
                string musteriNumarasi = "159753";
                string musteriSifre = "1234";
                string pricestr = Total.ToString().Replace(",", ".");
                string apiUrl = "https://localhost:44328/api/Pay?musterino=" + musteriNumarasi + "&musterisifre=" + musteriSifre + "&kartno=" + model.CardNumber + "&sonkullanmaay=" + model.ReqMonth + "&sonkullanmayil=" + model.ReqYear + "&cvv=" + model.Cvv + "&bakiye="+ pricestr;
                HttpClient client = new HttpClient();
                HttpResponseMessage response = client.PostAsync(apiUrl,null).Result;
                var result = response.Content.ReadAsStringAsync();
                if (result.Result == "\"101\"")
                {
                    foreach (Cart item in memberCart)
                    {
                        db.Carts.Remove(item);
                    }
                    db.SaveChanges();
                    return RedirectToAction("PaymentSuccess", "CheckOut");
                }
                if (result.Result == "\"901\"")
                {
                    ViewBag.hata = "Verilerden en az biri boş";
                }
                if (result.Result == "\"902\"")
                {
                    ViewBag.hata = "Bir Hata Oluştu";
                }
                if (result.Result == "\"801\"")
                {
                    ViewBag.hata = "Pos Müşterisi Bulunamadı";
                }
                if (result.Result == "\"802\"")
                {
                    ViewBag.hata = "Pos Müşterisi İnaktif";
                }
                if (result.Result == "\"701\"")
                {
                    ViewBag.hata = "Kart Bulunamadı";
                }
                if (result.Result == "\"702\"")
                {
                    ViewBag.hata = "Son Kullanma Tarihi Geçmiş";
                }
                if (result.Result == "\"703\"")
                {
                    ViewBag.hata = "Güvenlik Kodu Hatalı";
                }
                if (result.Result == "\"601\"")
                {
                    ViewBag.hata = "Bakiye Yetersiz";
                }
            }

            ViewBag.cart = memberCart;
            return View(model);
        }
    }
}