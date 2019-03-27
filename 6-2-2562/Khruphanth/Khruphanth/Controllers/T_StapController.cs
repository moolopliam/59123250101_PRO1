using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Khruphanth.Models;

namespace Khruphanth.Controllers
{
    public class T_StapController : Controller
    {
        private readonly ComCSDBEntities _db = new ComCSDBEntities();

        public ActionResult Index()
        {
            return View(_db.T_Stap.ToList());
        }

        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Stap t_Stap = _db.T_Stap.Find(id);
            if (t_Stap == null)
            {
                return HttpNotFound();
            }
            return View(t_Stap);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StepID,ST_StepName")] T_Stap t_Stap)
        {
            if (ModelState.IsValid)
            {
                _db.T_Stap.Add(t_Stap);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(t_Stap);
        }

        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Stap t_Stap = _db.T_Stap.Find(id);
            if (t_Stap == null)
            {
                return HttpNotFound();
            }
            return View(t_Stap);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StepID,ST_StepName")] T_Stap t_Stap)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(t_Stap).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(t_Stap);
        }

        // GET: T_Stap/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Stap t_Stap = _db.T_Stap.Find(id);
            if (t_Stap == null)
            {
                return HttpNotFound();
            }
            return View(t_Stap);
        }

        // POST: T_Stap/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            T_Stap t_Stap = _db.T_Stap.Find(id);
            if (t_Stap != null) _db.T_Stap.Remove(t_Stap);
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
