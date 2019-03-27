using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;

using System.Web.Mvc;
using Khruphanth.Models;
using Khruphanth.Inheritance;

namespace Khruphanth.Controllers
{
    public class RequisitionController : Controller
    {

        // GET: Requisition
        private readonly ComCSDBEntities _db = new ComCSDBEntities();

        public ActionResult Waiting()
        {

            var data = _db.T_Requisition.Where(x => x.Re_StepID == "0").ToList();
            TempData["amout"] = data.Count;
            return View(GetT_Requisitions());
        }
        public List<T_Requisition> GetT_Requisitions()
        {
            var data = _db.T_Requisition.Where(x => x.Re_StepID == "0").ToList();
            TempData["amout"] = data.Count;
            return _db.T_Requisition.ToList();
        }




        public ActionResult Index()
        {
            return View(_db.T_Requisition.ToList());
        }

        // GET: Requisition/Details/5

        public ActionResult Details(string RequisitionID)
        {
            if (RequisitionID != null)
            {
                Session["RequisitionID"] = RequisitionID;

            }
            else
            {
                return RedirectToAction(nameof(Waiting));
            }

            var data = _db.T_RequestList.Where(x => x.RL_RequisitionID == RequisitionID).ToList();
            Session["IX"] = 0;
            if (data.Count > 0)
            {
                Session["IX"] = 1;
            }
            return View(data);
        }

        public ActionResult DetailsS(string RequisitionID)
        {

            if (RequisitionID != null)
            {
                Session["RequisitionID"] = RequisitionID;

            }
            else
            {
                return RedirectToAction(nameof(Waiting));
            }
            var data = _db.T_RequestList.Where(x => x.RL_RequisitionID == RequisitionID).ToList();

            return View(data);
        }

        // GET: Requisition/Create
        public ActionResult Create()
        {
            var data = _db.Teacher.ToList();
            var value = new List<TmpTeacher>();
            foreach (var item in data)
            {
                value.Add(new TmpTeacher { IDT = item.TeaId, NAMEFULL = item.Title.TName + "     " + item.TeaName });
            }
            ViewBag.Re_TeaId = new SelectList(value, "IDT", "NAMEFULL");

            return View();
        }

        // POST: Requisition/Create
        [HttpPost]
        public ActionResult Create(T_Requisition data)
        {
          ConvertDate convertD = new ConvertDate();

         
            var data1 = _db.Teacher.ToList();
            var dateNow = convertD.ThaiDate(data.Re_DateRequi);
            data.Re_DateRequi = Convert.ToDateTime(dateNow);
            var value = new List<TmpTeacher>();
            foreach (var item in data1)
            {
                value.Add(new TmpTeacher { IDT = item.TeaId, NAMEFULL = item.Title.TName + "     " + item.TeaName });
            }
            ViewBag.Re_TeaId = new SelectList(value, "IDT", "NAMEFULL");
            if (Convert.ToInt32(data.RequisitionID) < 0)
            {
                ModelState.AddModelError("RequisitionID", "กรุณาตรวจสอบ กรุณากรอกอีกครั้ง");
                return View(data);
            }
            var Chk = _db.T_Requisition.Where(x => x.RequisitionID == data.RequisitionID).ToList();
            if (ModelState.IsValid)
            {
                try
                {
                    var p = int.TryParse(data.RequisitionID, out _);
                    if (p)
                    {
                        var Year = DateTime.Now.ToString("yy");
                        data.RequisitionID = data.RequisitionID + "/" + Year;
                        if (Chk.Count == 0)
                        {
                            data.Re_StepID = "0";
                            _db.T_Requisition.Add(data);
                            _db.SaveChanges();
                            Session["Result"] = "okC";
                            Session["RequisitionID"] = data.RequisitionID;
                            return RedirectToAction("Waiting", "Requisition");
                        }
                        else
                        {
                            ViewBag.Re_TeaId = new SelectList(value, "IDT", "NAMEFULL");
                            ModelState.AddModelError("RequisitionID", "เลขใบเบิกซ้ำ");
                            return View(data);
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("RequisitionID", "กรุณากรอกเลขที่ใบเบิกเป็นตัวเลข");
                        return View(data);
                    }
                }
                catch
                {
                    ModelState.AddModelError("RequisitionID", "เลขใบเบิกซ้ำ");
                    return View(data);
                }
            }
            else
            {
                return View(data);
            }

        }

        public JsonResult GetProductsByCategoryId(string id = "0")
        {
            var ID = Int32.Parse(id);

            List<T_Type> Type = new List<T_Type>();
            if (ID != 0)
            {
                Type = _db.T_Type.Where(p => p.TY_CategoryID == id).ToList();
                //Type.Insert(0, new T_Type { TypeID = 0, NameType = "กรุณาเลือกหมวดหมู่" });

            }
            else
            {
                Type.Insert(0, new T_Type { TypeID = "0", TY_NameType = "กรุณาเลือกหมวดหมู่" });
            }
            var result = (from r in Type
                          select new
                          {
                              id = r.TypeID,
                              name = r.TY_NameType
                          }).ToList();

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // GET: Requisition/Edit/5
        public ActionResult Edit(string RequisitionID)
        {

            ViewBag.Re_TeaId = new SelectList(_db.Teacher, "TeaId", "TeaName");
            if (RequisitionID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var data = _db.T_Requisition.FirstOrDefault(a => a.RequisitionID == RequisitionID);
            return View(data);
        }

        // POST: Requisition/Edit/5
        [HttpPost]
        public ActionResult Edit(T_Requisition data)
        {
            ViewBag.Re_TeaId = new SelectList(_db.Teacher, "TeaId", "TeaName");
            try
            {
                 data.Re_StepID = "0";
                _db.Entry(data).State = EntityState.Modified;
                _db.SaveChanges();
                Session["Result"] = "okE";
                return RedirectToAction(nameof(Waiting));
            }
            catch
            {
                return View(data);
            }
        }

        // GET: Requisition/Delete/5

        public ActionResult Delete(string RequisitionID = null)
        {

            if (RequisitionID == null)
            {
                Session["Result"] = "error";
                return View(nameof(Waiting));
            }
            var CHKDATA = _db.T_Requisition.FirstOrDefault(a => a.RequisitionID == RequisitionID);
            if (CHKDATA != null)
            {

                var RQLIST = _db.T_Requisition.FirstOrDefault(a => a.RequisitionID == RequisitionID);

                var LLIST = _db.T_RequestList.Where(x => x.RL_RequisitionID == RQLIST.RequisitionID).ToList();
                if (LLIST.Count == 0)
                {
                    if (RQLIST != null) _db.T_Requisition.Remove(RQLIST);
                    _db.SaveChanges();
                    Session["Result"] = "ok";
                    return RedirectToAction(nameof(Waiting));
                }
                else
                {
                    var CHK = _db.T_RequestList.FirstOrDefault(x => x.RL_RequisitionID == RequisitionID);
                    var Khruphanth = _db.T_Khruphanth.Where(v => v.Kh_RequestLsitID == CHK.RequestLsitID).ToList();
                    _db.T_Khruphanth.RemoveRange(Khruphanth);
                    _db.T_RequestList.RemoveRange(LLIST);
                    _db.SaveChanges();
                    Session["Result"] = "ok";
                    return RedirectToAction(nameof(Waiting));
                }

            }
            else
            {
                Session["Result"] = "error";
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //return View("Waiting"+GetT_Requisitions());
        }


    }
}
