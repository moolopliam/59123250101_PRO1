using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Khruphanth.Models;

namespace Khruphanth.Controllers
{
    public class DistributorController : Controller
    {
        private readonly ComCSDBEntities _db = new ComCSDBEntities();

        // GET: T_Category
        public ActionResult Index()
        {
            var data = _db.T_Distributor.ToList();
            return View(data);
        }
        public ActionResult IndexR()
        {
            var data = _db.T_Distributor.ToList();
            var a = _db.T_Distributor.Where(i => i.Di_Status == "3").ToList();
            TempData["a"] = a.Count;
            
            return View(data);
        }

        // GET: T_Category/Details/5
        public ActionResult Details(string DistributorID)
        {
            if (DistributorID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Session["CHK"] = 1;
            var data = _db.T_DistributorList.Where(a => a.DL_DistributorID == DistributorID).ToList();
            if (data.Count > 0)
            {
                Session["CHK"] = 0;
            }
            Session["IDD"] = DistributorID;
            return View(data);
        }
        public ActionResult DetailsR(string DistributorID)
        {


            var data = _db.T_DistributorList.Where(a => a.DL_DistributorID == DistributorID).ToList();

            return View(data);
        }
        public ActionResult Disbu(string DL)
        {
            if (DL != null)
            {
                var data = _db.T_Distributor.ToList();
                var Ex = data.FirstOrDefault(a => a.DistributorID == DL);
                if (Ex != null)
                {
                    Ex.Di_Status = "2";
                    _db.Entry(Ex).State = EntityState.Modified;
                    _db.SaveChanges();
                    Chang(DL);
                }
                return RedirectToAction(nameof(IndexR));
            }
            else
            {
                Session["Result"] = "error8";
                return RedirectToAction(nameof(IndexR));
            }
        }
        public void Chang(string ID)
        {
            var data = _db.T_DistributorList.Where(a => a.DL_DistributorID == ID).ToList();
            foreach (var item in data)
            {
                var KP = _db.T_Khruphanth.FirstOrDefault(a => a.KhruphanthID == item.DL_KhruphanthID);
                if (KP != null)
                {
                    KP.Kh_StatusID = 2;
                    _db.Entry(KP).State = EntityState.Modified;
                }
            }
            _db.SaveChanges();

        }


        public ActionResult DetailKhruphat(string KpID)
        {
            var result = _db.T_Khruphanth.FirstOrDefault(a => a.KhruphanthID == KpID);
            return View(result);
        }
        public ActionResult Khruphat()
        {
            var data = _db.T_Khruphanth.Where(a => a.Kh_StatusID == 1).ToList();
            return View(data);
        }

        public ActionResult Leave(string Khruphat)
        {
            var Kr = _db.T_Khruphanth.FirstOrDefault(a => a.KhruphanthID == Khruphat);

            var tmp = Session["IDD"].ToString();
            if (Kr != null)
            {
                T_DistributorList DList = new T_DistributorList()
                {
                    DL_DistributorID = tmp,
                    DL_KhruphanthID = Kr.KhruphanthID
                };
                _db.T_DistributorList.Add(DList);
            }

            if (Kr != null)
            {
                Kr.Kh_StatusID = 3;
                _db.Entry(Kr).State = EntityState.Modified;
            }

            _db.SaveChanges();
            Session["Result"] = "okC";
            Session.Remove("IDD");
            return RedirectToAction("Details", new { DistributorID = tmp });
        }

        // GET: T_Category/Create
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(T_Distributor data)
        {
            var dateNow = DateTime.Now.ToString("dd/MM/yyyy");
            data.Di_Date = Convert.ToDateTime(dateNow);
            if (ModelState.IsValid)
            {
                if (Convert.ToInt32(data.DistributorID) < 0)
                {
                    ModelState.AddModelError("DistributorID", "กรุณาตรวจสอบ กรุณากรอกอีกครั้ง");
                    return View(data);
                }

                var p = int.TryParse(data.DistributorID, out _);
                if (p)
                {
                    var Year = DateTime.Now.ToString("yy");
                    data.DistributorID = data.DistributorID + "/" + Year;
                    var chk = _db.T_Distributor.FirstOrDefault(c => c.DistributorID == data.DistributorID);
                    if (chk == null)
                    {

                        data.Di_Status = "3";
                        _db.T_Distributor.Add(data);
                        _db.SaveChanges();
                        Session["Result"] = "okC";
                        return RedirectToAction("IndexR");
                    }
                    else
                    {
                        ModelState.AddModelError("DistributorID",
                            "มีข้อมูลนี้อยู่ในฐานข้อมูลแล้ว กรุณาตรวจสอบอีกครั้ง");
                        return View(data);
                    }
                }
                else
                {
                    ModelState.AddModelError("DistributorID", "กรุณากรอกเลขที่ใบจำหน่ายเป็นตัวเลข");
                    return View(data);
                }

            }

            return View(data);
        }

        // GET: T_Category/Edit/5
        public ActionResult Edit(string DistributorID)
        {

            var t_Category = _db.T_Distributor.Where(a => a.DistributorID == DistributorID).FirstOrDefault();
            return View(t_Category);
        }

        // POST: T_Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public ActionResult Edit(T_Distributor data)
        {
            if (ModelState.IsValid)
            {
                _db.Entry(data).State = EntityState.Modified;
                _db.SaveChanges();
                Session["Result"] = "okE";
                return RedirectToAction("Index");
            }
            return View(data);
        }

        public ActionResult Delete(string DistributorID)
        {
            var data = _db.T_Distributor.FirstOrDefault(a => a.DistributorID == DistributorID);
            var chk = _db.T_DistributorList.Where(a => a.DL_DistributorID == data.DistributorID).ToList();
            try
            {
                if (chk.Count == 0)
                {
                    if (data != null)
                    {
                        _db.T_Distributor.Remove(data);
                        _db.SaveChanges();
                    }
                    Session["Result"] = "ok";
                    return RedirectToAction("IndexR");
                }
                else
                {
                    if (data != null)
                    {
                        _db.T_Distributor.Remove(data);
                        _db.T_DistributorList.RemoveRange(chk);
                        _db.SaveChanges();
                    }
                    Session["Result"] = "ok";
                    return RedirectToAction("IndexR");
                }
            }
            catch (Exception)
            {

                Session["Result"] = "error";
                return RedirectToAction("IndexR");
            }

        }

    }
}
