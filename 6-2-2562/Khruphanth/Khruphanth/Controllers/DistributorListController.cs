using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Khruphanth.Models;

namespace Khruphanth.Controllers
{
    public class DistributorListController : Controller
    {
        private readonly ComCSDBEntities _db = new ComCSDBEntities();
        // GET: Distributor
        public ActionResult Index()
        {
            var data = _db.T_DistributorList.ToList();
            return View(data);
        }

        // GET: Distributor/Details/5
        public ActionResult Details(int id)
        {
            var data = _db.T_DistributorList.FirstOrDefault(a => a.DistributorList == id);
            return View(data);
        }

        // GET: Distributor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Distributor/Create
        [HttpPost]
        public ActionResult Create(T_DistributorList data)
        {
            try
            {
                _db.T_DistributorList.Add(data);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(data);
            }
        }

        // GET: Distributor/Edit/5
        public ActionResult Edit(int id)
        {
            var data = _db.T_DistributorList.FirstOrDefault(a => a.DistributorList == id);
            return View(data);
        }

        // POST: Distributor/Edit/5
        [HttpPost]
        public ActionResult Edit(T_DistributorList data)
        {
            try
            {
                _db.Entry(data).State = EntityState.Modified;
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            try
            {
                var data = _db.T_DistributorList.FirstOrDefault(a => a.DistributorList == id);
                if (data != null)
                {
                    _db.T_DistributorList.Remove(data);
                }
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(nameof(Index));
            }
        }
    }
}
