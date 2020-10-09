using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.DATA.EF;
using System.ComponentModel.DataAnnotations;

namespace SAT.UI.MVC.Controllers
{

    public class CoursesController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: Courses
        [Authorize(Roles = "Admin")]
        public ActionResult Active()
        {

            return View(db.Courses1.ToList());

        }

        [Authorize(Roles = "Admin")]
        public ActionResult Retired()
        {
            return View(db.Courses1.ToList());
        }

        // GET: Courses/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses1.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CouseId,CourseName,CourseDescription,CreditHours,Curriculum,Notes,IsActive")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Courses1.Add(course);
                db.SaveChanges();
                if (course.IsActive == true)
                {
                    return RedirectToAction("Active");
                }
                else
                {
                    return RedirectToAction("Retired");
                }
            }

            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses1.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CouseId,CourseName,CourseDescription,CreditHours,Curriculum,Notes,IsActive")] Course course)
        {
            if (ModelState.IsValid)
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
                if (course.IsActive == true)
                {
                    return RedirectToAction("Active");
                }
                else
                {
                    return RedirectToAction("Retired");
                }
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Course course = db.Courses1.Find(id);
            if (course == null)
            {
                return HttpNotFound();
            }
            return View(course);
        }

        // POST: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Course course = db.Courses1.Find(id);
            course.IsActive = !course.IsActive;
            db.SaveChanges();
            if (course.IsActive == true)
            {
                return RedirectToAction("Active");
            }
            else
            {
                return RedirectToAction("Retired");
            }
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
