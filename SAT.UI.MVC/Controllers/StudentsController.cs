using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SAT.DATA.EF;

namespace SAT.UI.MVC.Controllers
{
    public class StudentsController : Controller
    {
        private SATEntities db = new SATEntities();

        // GET: Students
        [Authorize(Roles = "Admin, Scheduling")]
        public ActionResult Index()
        {
            var students = db.Students.Include(s => s.StudentStatus);
            return View(students.ToList());
        }

        // GET: Students/Details/5
        [Authorize(Roles = "Admin, Scheduling")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student, HttpPostedFileBase studentPic)
        {
            if (ModelState.IsValid)
            {

                //Only process the image if ALL other Model information is valid
                #region File Upload - Game Cover
                //Use a default image if one is not provided with the book object - noImage.png
                string imgName = "noImage.png";

                //check the HPFB to ensure it is NOT NUll
                if (studentPic != null)
                {
                    //if not null do the following
                    //retrieve the image from the HPFB and assign to the variable
                    imgName = studentPic.FileName;

                    //declare and assign the extension.
                    string ext = imgName.Substring(imgName.LastIndexOf('.'));

                    //declare list of VALID extensions (image types)
                    string[] goodExts = { ".jpeg", ".jpg", ".gif", ".png" };

                    //check the ext variable using the ToLower() against the valid list && make sure the content length is not too large.
                    if (goodExts.Contains(ext.ToLower()) && (studentPic.ContentLength <= 41964304))
                    //Content Length is 4mb max allowed by ASP.NET (expressed in bytes)
                    {

                        //if both values are good rename using a GUID (there are other ways to rename the file) + ext to the new name
                        //GUID - Global Unique IDentifier
                        imgName = Guid.NewGuid() + ext.ToLower();//ToLower() is optional, just makes files look cleaner in a list
                                                                 //could rename the file with unique information, you just need to ensure that the stringLength will accommodate                        it.
                                                                 //imgName = book.BookTitle + "_" + DateTime.Now + "_" + User.Identity.Name;
                                                                 //string length for image in metadata should accommodate StringLength for Booktitle + MAX dateTime Characters +                        128
                                                                 //characters for the username. - Would fail if an ANONYMOUS (unauthenticated user added a book record with                             image)

                        #region Save UnResized value to the webserver
                        //save the value to the webserver
                        studentPic.SaveAs(Server.MapPath("~/Content/assets/images/studentImages/" + imgName));
                        #endregion

                        #region Resize Image **inactive Code**
                        //provide the requirements to call the ResizeImage() - SavePath, Image, maxImageSize, MaxThumb Size
                        //string savePath = Server.MapPath("~/Content/imgstore/books/");
                        //Image convertedImage = Image.FromStream(studentPic.InputStream);
                        //int maxImageSize = 500;
                        //int maxThumbSize = 100;

                        ////Call the ImageService.ResizeImage()
                        //ImageService.ResizeImage(savePath, imgName, convertedImage, maxImageSize, maxThumbSize);

                        #endregion
                        //No Matter What - add the imageName to the Property of the Book object to send to the database.
                        student.PhotoUrl = imgName;
                    }
                    //if EITHER of the values are NOT valid - go back to the noImage.png
                    else
                    {
                        imgName = "noImage.png";
                    }

                }

                #endregion

                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,FirstName,LastName,Major,Address,City,State,ZipCode,Phone,Email,PhotoUrl,SSID")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SSID = new SelectList(db.StudentStatuses, "SSID", "SSName", student.SSID);
            return View(student);
        }

        // GET: Students/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
