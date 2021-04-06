using Microsoft.AspNet.Identity;
using NewYearCard.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewYearCard.Controllers
{
    public class CardsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Cards
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New(string PhotoPath)
        {
            ViewBag.PhotoPath = PhotoPath;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult New(Card card)
        {
            if (ModelState.IsValid)
            {
                string id = User.Identity.GetUserId();
                var user = db.Users.FirstOrDefault(x => x.Id == id);
                user.UserCards.Add(card);
                db.Cards.Add(card);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Delete(int id)
        {
            string userId = User.Identity.GetUserId();
            var user = db.Users.FirstOrDefault(x => x.Id == userId);
            var card = db.Cards.FirstOrDefault(x => x.Id == id);
            user.UserCards.Remove(card);
            db.Cards.Remove(card);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Edit(int? id)
        {

            var kart = db.Cards.FirstOrDefault(x => x.Id == id);
            if (kart == null)
            {
                return HttpNotFound();
            }
            return View(kart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(card);
        }


        public ActionResult Download(int id)
        {
            var kart = db.Cards.FirstOrDefault(x => x.Id == id);
            string deneme = kart.Message;
            string mesaj1;
            string mesaj2;
            if (deneme.Length > 200)
            {
                mesaj1 = deneme.Substring(0, deneme.Length / 2);
                mesaj2 = deneme.Substring((deneme.Length / 2) + 1);
            }
            else
            {
                mesaj1 = deneme;
                mesaj2 = "";
            }


            Bitmap bmp = new Bitmap(Server.MapPath($"~/images/{kart.PhotoPath}.png"));
            Bitmap bmp2 = ResizeBitmap(bmp, 420, 330);
            Bitmap bmp3 = new Bitmap(1000, 1000);
            Graphics g = Graphics.FromImage(bmp3);
            g.FillRectangle(Brushes.LightGray, 0, 0, bmp3.Width, bmp3.Height);
            g.DrawImage(bmp2, new PointF(300, 500));
            g.DrawString($"Sevgili {kart.ReciverName},", new Font("Arial", 22, FontStyle.Bold), new SolidBrush(Color.DarkRed), new PointF(100, 100));

            g.DrawString($"{mesaj1}", new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(30, 200));
            g.DrawString($"{mesaj2}", new Font("Arial", 12, FontStyle.Bold), new SolidBrush(Color.Black), new PointF(30, 250));



            g.DrawString($"{kart.SenderName}", new Font("Arial", 22, FontStyle.Bold), new SolidBrush(Color.DarkRed), new PointF(600, 470));
            byte[] data;
            using (MemoryStream ms = new MemoryStream())
            {

                bmp3.Save(ms, ImageFormat.Jpeg);
                data = ms.ToArray();
            }
            string fileName = "resim.jpeg";
            return File(data, "image/jpeg", fileName);


        }

        public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            using (Graphics g = Graphics.FromImage(result))
            {
                g.DrawImage(bmp, 0, 0, width, height);
            }

            return result;
        }

        public ActionResult Display(int id)
        {
            var kart = db.Cards.FirstOrDefault(x => x.Id == id);
            if (kart == null)
            {
                return HttpNotFound();
            }
            return PartialView("_CardDiv", kart);

        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}