using System.Data.Entity;
using System.Net;


using System;
using System.Linq;
using System.Web.Mvc;
using CarInsurance.Models;

namespace CarInsurance.Controllers
{
    public class InsureeController : Controller
    {
        private insuranceEntities db = new insuranceEntities();

        // GET: Insuree
        public ActionResult Index()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Insuree/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,SpeedingTickets,DUI,CoverageType")] Insuree insuree)
        {

            if (ModelState.IsValid)
            {
                insuree.Quote = CalculateQuote(insuree);


                db.Insurees.Add(insuree);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(insuree);
        }

        // GET: Insuree/Admin
        public ActionResult Admin()
        {
            return View(db.Insurees.ToList());
        }

        // GET: Insuree/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null) return HttpNotFound();

            return View(insuree);
        }

        // GET: Insuree/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null) return HttpNotFound();

            return View(insuree);
        }

        // POST: Insuree/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,DateOfBirth,CarYear,CarMake,CarModel,SpeedingTickets,DUI,CoverageType")] Insuree insuree)
        {
            if (ModelState.IsValid)
            {
                insuree.Quote = CalculateQuote(insuree); // keep your quote logic

                db.Entry(insuree).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(insuree);
        }

        // GET: Insuree/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            Insuree insuree = db.Insurees.Find(id);
            if (insuree == null) return HttpNotFound();

            return View(insuree);
        }

        // POST: Insuree/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Insuree insuree = db.Insurees.Find(id);
            db.Insurees.Remove(insuree);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        private decimal CalculateQuote(Insuree insuree)
        {
            decimal monthlyTotal = 50m;

            DateTime dob = insuree.DateOfBirth ?? DateTime.Today;
            int age = CalculateAge(dob);

            if (age <= 18) monthlyTotal += 100m;
            else if (age <= 25) monthlyTotal += 50m;
            else monthlyTotal += 25m;

            int carYear = insuree.CarYear ?? 0;
            if (carYear != 0 && carYear < 2000) monthlyTotal += 25m;
            if (carYear > 2015) monthlyTotal += 25m;

            if (!string.IsNullOrWhiteSpace(insuree.CarMake) &&
                insuree.CarMake.Trim().Equals("Porsche", StringComparison.OrdinalIgnoreCase))
            {
                monthlyTotal += 25m;

                if (!string.IsNullOrWhiteSpace(insuree.CarModel) &&
                    insuree.CarModel.Trim().Equals("911 Carrera", StringComparison.OrdinalIgnoreCase))
                {
                    monthlyTotal += 25m;
                }
            }

            int tickets = insuree.SpeedingTickets ?? 0;
            monthlyTotal += tickets * 10m;

            if (insuree.DUI == true) monthlyTotal *= 1.25m;
            if (insuree.CoverageType == true) monthlyTotal *= 1.50m;

            return decimal.Round(monthlyTotal, 2);
        }

        private int CalculateAge(DateTime dob)
        {
            int age = DateTime.Today.Year - dob.Year;
            if (dob.Date > DateTime.Today.AddYears(-age)) age--;
            return age;
        }

    }
}

