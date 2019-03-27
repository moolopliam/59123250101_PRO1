using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Khruphanth.Models;

namespace Khruphanth.Controllers
{
    public class T_PlaceController : Controller
    {
        private readonly ComCSDBEntities _db = new ComCSDBEntities();

        public ActionResult Index()
        {
            return View(_db.T_Place.ToList());
        }

        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var T_Place = _db.T_Place.FirstOrDefault(a => a.PlaceID == id);
            if (T_Place == null)
            {
                return HttpNotFound();
            }
            return View(T_Place);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(T_Place data)
        {
            if (ModelState.IsValid)
            {
                var chk = _db.T_Place.FirstOrDefault(c => c.PlaceID == data.PlaceID);
                if (chk == null)
                {

                    _db.T_Place.Add(data);
                    _db.SaveChanges();
                    Session["Result"] = "okC";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CategoryID", "มีหมวดนี้อยู่ในฐานข้อมูลแล้ว กรุณาตรวจสอบอีกครั้ง");
                }

            }

            return View(data);
        }

        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var T_Place = _db.T_Place.FirstOrDefault(a => a.PlaceID == id);
            if (T_Place == null)
            {
                return HttpNotFound();
            }
            return View(T_Place);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_Place T_Place)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(T_Place).State = EntityState.Modified;
                _db.SaveChanges();
                Session["Result"] = "okE";
                return RedirectToAction("Index");
            }
            return View(T_Place);
        }

        public ActionResult Delete(int PlaceID)
        {
            var data = _db.T_Place.FirstOrDefault(a => a.PlaceID == PlaceID);
            var chk = _db.T_Khruphanth.FirstOrDefault(a => a.Kh_PlaceID == PlaceID);
            if (chk == null)
            {
                if (data != null) _db.T_Place.Remove(data);
                _db.SaveChanges();
                Session["Result"] = "ok";
                return RedirectToAction("Index");
            }
            Session["Result"] = "error";
            return RedirectToAction("Index");
        }
    }
}
