using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Khruphanth.Models;

namespace Khruphanth.Controllers
{
    public class TypeController : Controller
    {
        private readonly ComCSDBEntities _db = new ComCSDBEntities();

        // GET: T_Category
        public ActionResult Index()
        {
            var data = _db.T_Type.ToList();
            return View(data);
        }

        // GET: T_Category/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Type T_Type = _db.T_Type.Find(id);
            if (T_Type == null)
            {
                return HttpNotFound();
            }
            return View(T_Type);
        }

        // GET: T_Category/Create
        public ActionResult Create()
        {
            ViewBag.TY_CategoryID = new SelectList(_db.T_Category, "CategoryID", "CA_NameCategory");
            return View();
        }

        [HttpPost]
        public ActionResult Create(T_Type T_Type)
        {
            ViewBag.TY_CategoryID = new SelectList(_db.T_Category, "CategoryID", "CA_NameCategory");
            if (ModelState.IsValid)
            {
                var chk = _db.T_Type.FirstOrDefault(c => c.TypeID == T_Type.TypeID);
                if (chk == null)
                {
                    var p = int.TryParse(T_Type.TypeID, out _);
                    if (p)
                    {
                        _db.T_Type.Add(T_Type);
                        _db.SaveChanges();
                        Session["Result"] = "okC";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("TypeID", "กรุณากรอกเป็นตัวเลข");
                        return View(T_Type);
                    }
                }
                else
                {
                    ModelState.AddModelError("TypeID", "มีหมวดนี้อยู่ในฐานข้อมูลแล้ว กรุณาตรวจสอบอีกครั้ง");
                }

            }

            return View(T_Type);
        }

        // GET: T_Category/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Type T_Type = _db.T_Type.Find(id);
            if (T_Type != null)
            {
                ViewBag.TY_CategoryID = new SelectList(_db.T_Category, "CategoryID", "CA_NameCategory", T_Type.TypeID);

            }
            return View(T_Type);
        }

        // POST: T_Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(T_Type T_Type)
        {
            ViewBag.TY_CategoryID = new SelectList(_db.T_Category, "CategoryID", "CA_NameCategory", T_Type.TypeID);
            if (ModelState.IsValid)
            {
                var p = int.TryParse(T_Type.TypeID, out _);
                if (p)
                {
                    _db.Entry(T_Type).State = EntityState.Modified;
                    _db.SaveChanges();
                    Session["Result"] = "okE";
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("TypeID", "กรุณากรอกเป็นตัวเลข");
                    return View();
                }
            }
            return View(T_Type);
        }

        public ActionResult Delete(string id)
        {
            var data = _db.T_Type.FirstOrDefault(a => a.TypeID.Contains(id));
            var chk = _db.T_RequestList.FirstOrDefault(a => a.RL_TypeID.Contains(id));
            if (chk == null)
            {
                if (data != null) _db.T_Type.Remove(data);
                _db.SaveChanges();
                Session["Result"] = "ok";
                return RedirectToAction("Index");
            }
            Session["Result"] = "error";
            return RedirectToAction("Index");
        }
    }
}
