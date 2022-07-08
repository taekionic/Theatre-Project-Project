using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using TheatreCMS3.Areas.Rent.Models;
using TheatreCMS3.Models;
using System.IO;
using System.Drawing;

namespace TheatreCMS3.Areas.Rent
{
    public class RentalItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rent/RentalItems
        public async Task<ActionResult> Index(string searchString)
        {
            var Rentals = from r in db.RentalItems
                          select r;

            if (!String.IsNullOrEmpty(searchString))
            {
                Rentals = Rentals.Where(s => s.Item.Contains(searchString) || s.ItemDescription.Contains(searchString));
            }
            return View(await Rentals.ToListAsync());
        }

        // GET: Rent/RentalItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalItem rentalItem = db.RentalItems.Find(id);
            if (rentalItem == null)
            {
                return HttpNotFound();
            }
            return View(rentalItem);
        }

        // GET: Rent/RentalItems/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rent/RentalItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalItemId,Item,ItemDescription,PickupDate,ReturnDate,ItemPhoto")] RentalItem rentalItem, HttpPostedFileBase userimage)
        {
            if (ModelState.IsValid)
            {

                db.RentalItems.Add(rentalItem);
                db.SaveChanges(); 
                return RedirectToAction("Index");
            }

            return View(rentalItem);
        }

        // GET: Rent/RentalItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalItem rentalItem = db.RentalItems.Find(id);
            if (rentalItem == null)
            {
                return HttpNotFound();
            }
            //creating a temporary 
            TempData["image"] = rentalItem;

            return View(rentalItem);
            
        }

        // POST: Rent/RentalItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalItemId,Item,ItemDescription,PickupDate,ReturnDate,ItemPhoto")] RentalItem rentalItem, HttpPostedFileBase userimage)
        {
            //instanating a temporary object of class RentalItem to ensure that when form is submitted, either the original image is saved along with the form, or a new image is uploaded depending on user request.
            //If original image byte array is null, we find the rental item to later save. If the image field is not empty,  we save the template of the instantiated object to later update.
            RentalItem orginalimg = TempData["image"] == null ? db.RentalItems.Find(rentalItem.RentalItemId) : (RentalItem)TempData["image"];
            if (ModelState.IsValid)
            {

                if (userimage != null)
                {
                    var updateimage = rentalimage(userimage);
                    rentalItem.ItemPhoto = updateimage;
                }

                if (userimage == null && rentalItem.ItemPhoto == null && orginalimg.ItemPhoto != null)
                {
                    rentalItem.ItemPhoto = orginalimg.ItemPhoto;
                }

                db.Entry(rentalItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rentalItem);
        }

        // GET: Rent/RentalItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalItem rentalItem = db.RentalItems.Find(id);
            if (rentalItem == null)
            {
                return HttpNotFound();
            }
            return View(rentalItem);
        }

        // POST: Rent/RentalItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentalItem rentalItem = db.RentalItems.Find(id);
            db.RentalItems.Remove(rentalItem);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //public byte[] RentalImg(HttpPostedFileBase userimage)
        //{
        //    byte[] imgArray;
        //    using (BinaryReader reader = new BinaryReader(userimage.InputStream))
        //    {
        //        imgArray = reader.ReadBytes(userimage.ContentLength);
        //    }
        //    return imgArray;
        //}

        [HttpPost]
        public byte[] rentalimage(HttpPostedFileBase userimage)
        {
            byte[] fileByte = new byte[userimage.ContentLength];
            userimage.InputStream.Read(fileByte, 0, userimage.ContentLength);
            return fileByte;
        }


    }
}
