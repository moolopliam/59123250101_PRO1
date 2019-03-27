using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Khruphanth.Models;

namespace Khruphanth.Controllers
{
    public class T_StatusController : Controller
    {
        private readonly ComCSDBEntities _db = new ComCSDBEntities();

  
        public ActionResult Index()
        {
            return View(_db.T_Status.ToList());
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Status t_Status = _db.T_Status.Find(id);
            if (t_Status == null)
            {
                return HttpNotFound();
            }
            return View(t_Status);
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusID,Status_Name")] T_Status t_Status)
        {
            if (ModelState.IsValid)
            {
                _db.T_Status.Add(t_Status);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_Status);
        }


        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Status t_Status = _db.T_Status.Find(id);
            if (t_Status == null)
            {
                return HttpNotFound();
            }
            return View(t_Status);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusID,Status_Name")] T_Status t_Status)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(t_Status).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_Status);
        }

        // GET: T_Status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Status t_Status = _db.T_Status.Find(id);
            if (t_Status == null)
            {
                return HttpNotFound();
            }
            return View(t_Status);
        }

        // POST: T_Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            T_Status t_Status = _db.T_Status.Find(id);
            if (t_Status != null) _db.T_Status.Remove(t_Status);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
