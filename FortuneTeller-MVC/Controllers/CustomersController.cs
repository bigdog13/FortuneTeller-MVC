using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FortuneTeller_MVC.Models;

namespace FortuneTeller_MVC.Controllers
{
    public class CustomersController : Controller
    {
        private Entities db = new Entities();

        // GET: Customers
        public ActionResult Index()
        {
            var customers = db.Customers.Include(c => c.Color).Include(c => c.Month);
            return View(customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                

                if (customer.Age % 2 == 0)
                {   //even
                    ViewBag.NumberOfYears = 20;
                }
                else
                {
                    //odd
                    ViewBag.NumberOfYears = 35;
                }
                
            }
            //customer retirement w/ amount of money

            int sub = customer.FirstName.IndexOf(customer.Month.Description[0]);
            int sub2 = customer.FirstName.IndexOf(customer.Month.Description[2]);
            int sub3 = customer.FirstName.IndexOf(customer.Month.Description[3]);



            int sub16 = customer.LastName.IndexOf(customer.Month.Description[0]);
            int sub17 = customer.LastName.IndexOf(customer.Month.Description[1]);
            int sub18 = customer.LastName.IndexOf(customer.Month.Description[2]);




            if (sub != -1 && sub16 != -1)
            {
                @ViewBag.AmountOfMoney = 50;
            }
            else if (sub2 != -1 && sub17 != -1)
            {
                @ViewBag.AmountOfMoney = 50000;
            }
            else if (sub3 != -1 && sub18 != -1)
            {
                @ViewBag.AmountOfMoney = 1000000;
            }

            else
            {
                ViewBag.AmountOfMoney = 1;
            }

            while (customer.Color.Description == "RED")
            {
                @ViewBag.ModeOfTransportation = "Royal Carriage";

            }
            while (customer.Color.Description == "ORANGE")
            {
                @ViewBag.ModeOfTransportation = "Magical Pumpkin";

            }
            while (customer.Color.Description == "YELLOW")
            {
                @ViewBag.ModeOfTransportation = "Magical Broom Stick";

            }
            while (customer.Color.Description == "GREEN")
            {
                @ViewBag.ModeOfTransportation = "Magical Fairy";

            }
            while (customer.Color.Description == "BLUE")
            {
                @ViewBag.ModeOfTransportation = "Magical Carpet";

            }
            while (customer.Color.Description == "INDIGO")
            {
                @ViewBag.ModeOfTransportation = "Millennium Falcon";

            }
            while (customer.Color.Description == "VIOLET")
            {
                @ViewBag.ModeOfTransportation = "Horse and Buggy";

            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Description");
            ViewBag.MonthID = new SelectList(db.Months, "ID", "Description");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FirstName,LastName,Age,MonthID,ColorID,Siblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                
                return RedirectToAction("Details", new { id = customer.ID });
            }

            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Description", customer.ColorID);
            ViewBag.MonthID = new SelectList(db.Months, "ID", "Description", customer.MonthID);
            return View(customer);
        }
        
        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Description", customer.ColorID);
            ViewBag.MonthID = new SelectList(db.Months, "ID", "Description", customer.MonthID);
            return View(customer);
        }
        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FirstName,LastName,Age,MonthID,ColorID,Siblings")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ColorID = new SelectList(db.Colors, "ID", "Description", customer.ColorID);
            ViewBag.MonthID = new SelectList(db.Months, "ID", "Description", customer.MonthID);
            return View(customer);
        }
        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }
        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
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

