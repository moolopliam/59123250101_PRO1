using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Khruphanth.Models;

namespace Khruphanth.Controllers
{
    public class RequisRestController : Controller
    {
        private readonly ComCSDBEntities _db = new ComCSDBEntities();
        // GET: RequisRest
        public ActionResult Index()
        {
            var data = _db.T_Requisition.Where(a => a.Re_StepID == "1").ToList();
            return View(data);
        }

        public ActionResult Details(string RequisitionID)
        {
            var data = _db.T_RequestList.Where(a => a.RL_RequisitionID == RequisitionID).ToList();
            foreach (var item in data)
            {
                var KP = _db.T_Khruphanth.Where(a => a.Kh_RequestLsitID == item.RequestLsitID && a.Kh_StatusID == 2).ToList();
                item.RL_Amount -= KP.Count;
            }
            return View(data);
        }

        public ActionResult DetailsK(int id)
        {
            var data = _db.T_RequestList.FirstOrDefault(x => x.RequestLsitID == id);
            return View(data);
        }

  
    }
}