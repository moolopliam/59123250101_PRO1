using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Khruphanth.Models;

namespace Khruphanth.Controllers
{
    public class T_CategoryController : Controller
    {
        private readonly ComCSDBEntities _db = new ComCSDBEntities();

        // GET: T_Category
        public ActionResult Index()
        {
            return View(_db.T_Category.ToList());
        }

        // GET: T_Category/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Category t_Category = _db.T_Category.Find(id);
            if (t_Category == null)
            {
                return HttpNotFound();
            }
            return View(t_Category);
        }

        // GET: T_Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: T_Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(T_Category t_Category)
        {
            if (ModelState.IsValid)
            {
                var chk = _db.T_Category.FirstOrDefault(c => c.CategoryID == t_Category.CategoryID);
                if (chk == null)
                {

                    var p = int.TryParse(t_Category.CategoryID, out _);
                    if (p)
                    {
                    _db.T_Category.Add(t_Category);
                    _db.SaveChanges();
                    Session["Result"] = "okC";
                    return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError("CategoryID", "กรุณากรอกเป็นตัวเลข");
                        return View(t_Category);
                    }
                }
                else
                {
                    ModelState.AddModelError("CategoryID", "มีหมวดนี้อยู่ในฐานข้อมูลแล้ว กรุณาตรวจสอบอีกครั้ง");
                    return View(t_Category);
                }

            }

            return View(t_Category);
        }

        // GET: T_Category/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T_Category t_Category = _db.T_Category.Find(id);
            if (t_Category == null)
            {
                return HttpNotFound();
            }
            return View(t_Category);
        }

        // POST: T_Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CA_NameCategory")] T_Category t_Category)
        {
            if (ModelState.IsValid)
            {
                var p = int.TryParse(t_Category.CategoryID, out _);
                if (p)
                {
                    _db.Entry(t_Category).State = EntityState.Modified;
                _db.SaveChanges();
                Session["Result"] = "okE";
                return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("CategoryID", "กรุณากรอกเป็นตัวเลข");
                    return View();
                }
            }
            return View(t_Category);
        }

        public ActionResult Delete(string CategoryID)
        {
            var data = _db.T_Category.FirstOrDefault(a => a.CategoryID.Contains(CategoryID));
            var chk = _db.T_Type.FirstOrDefault(a => a.TypeID.Contains(CategoryID));
            if (chk == null)
            {
                if (data != null) _db.T_Category.Remove(data);
                _db.SaveChanges();
                Session["Result"] = "ok";
                return RedirectToAction("Index");
            }
            Session["Result"] = "error";
            return RedirectToAction("Index");
        }

    }
}
