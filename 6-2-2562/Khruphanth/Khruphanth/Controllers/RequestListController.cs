using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Khruphanth.Models;
using QRCoder;
using System.Drawing;
using System.Data.Entity;

namespace Khruphanth.Controllers
{
    public class RequestListController : Controller
    {
        // GET: RequestList
        private readonly ComCSDBEntities _db = new ComCSDBEntities();
        public readonly List<PhatQR> PhatQRs = new List<PhatQR>();

        public ActionResult Details(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Waiting", "Requisition");
            }
            var data = _db.T_RequestList.FirstOrDefault(x => x.RequestLsitID == id);
            return View(data);
        }

        // GET: RequestList/Create
        public ActionResult Create(string RequestLsitID)
        {
            if (RequestLsitID != null)
            {
                Session["number"] = RequestLsitID;
            }
            // ReSharper disable once CollectionNeverUpdated.Local
            List<T_Type> _types = new List<T_Type>();

            //LstType.Insert(0, new T_Type { TypeID = "0", TY_NameType = "----เลือกชนิด----" });

            List<T_Category> ListCategory = _db.T_Category.ToList();
            //ListCategory.Insert(0, new T_Category { CategoryID = "0", CA_NameCategory = "-กรุณาเลือกหมวด-" });

            ViewBag.RL_CategoryID = new SelectList(ListCategory, "CategoryID", "CA_NameCategory");
            ViewBag.RL_TypeID = new SelectList(_types, "TypeID", "TY_NameType");
            ViewBag.RL_PlaceID = new SelectList(_db.T_Place, "PlaceID", "PL_NamePlace");
            return View();
        }

        // POST: RequestList/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(T_RequestList data)
        {
            ViewBag.RL_CategoryID = new SelectList(_db.T_Category, "CategoryID", "CA_NameCategory");
            ViewBag.RL_TypeID = new SelectList(_db.T_Type, "TypeID", "TY_NameType");
            ViewBag.RL_PlaceID = new SelectList(_db.T_Place, "PlaceID", "PL_NamePlace");
            var chk = _db.T_Requisition.FirstOrDefault(x => x.RequisitionID == data.RL_RequisitionID);

            if (data.RL_Amount < 1)
            {
                ModelState.AddModelError("RL_Amount", "กรุณากรอกข้อมูล ให้มากกว่า 0");
            }
            else if (data.RL_OnStart < 1)
            {
                ModelState.AddModelError("RL_OnStart", "กรุณากรอกข้อมูล ให้มากกว่า 0");
            }
            else if (data.RL_Price < 1)
            {
                ModelState.AddModelError("RL_Price", "กรุณากรอกข้อมูล ให้มากกว่า 0");
            }
            else
            {
                if (ModelState.IsValid)
                {

                    if (chk != null)
                    {
                        if (data.ImageUpload != null)
                        {
                            string fileName = Path.GetFileNameWithoutExtension(data.ImageUpload.FileName);
                            string extension = Path.GetExtension(data.ImageUpload.FileName);
                            fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                            data.RL_PictureKhru = "~/Images/" + fileName;
                            data.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/"), fileName));
                        }

                        var str = DateTime.Now.ToString("yy");

                        var TmpCODE = "";
                        for (int i = 0; i < data.RL_Amount; i++)
                        {
                            TmpCODE = data.RL_CategoryID + "." + data.RL_TypeID + "." + (data.RL_OnStart + i) + "/" + str;
                        }

                        var _VALUE = _db.T_RequestList.ToList();
                        List<string> list = new List<string>();
                        List<string> ListCK = new List<string>();
                        int p = 0;
                        foreach (var item in _VALUE)
                        {
                            p++;
                            list.Add(item.RL_CategoryID+"."+item.RL_TypeID+"."+(item.RL_OnStart+p)+"/"+ chk.Re_DateRequi.Value.ToString("yy"));
                        }

                        for (int i = 0; i < data.RL_Amount; i++)
                        {
                            ListCK.Add(data.RL_CategoryID+"."+data.RL_TypeID+"."+(data.RL_OnStart+i)+"/"+chk.Re_DateRequi.Value.ToString("yy"));
                        }

                        int X = 0;
                        foreach (var item in list)
                        {
                            foreach (var item2 in ListCK)
                            {
                                if (item2 == item)
                                {
                                    X++;
                                }
                            }
                            
                        }

                        if (X == 0)
                        {
                            _db.T_RequestList.Add(data);
                            _db.SaveChanges();
                            Session["Result"] = "okC";
                            return RedirectToAction("Details", "Requisition", new { RequisitionID = data.RL_RequisitionID });
                        }
                        else
                        {
                        ModelState.AddModelError("RL_OnStart", "เลขครุภัณฑ์ซ้ำ กรุณาตรวจสอบอีกครั้ง");

                        }
                    }
                    else
                    {
                        ModelState.AddModelError("RL_RequisitionID", "ไม่พบข้อมูลเลขการเบิก");
                    }

                }
            }

            return View(data);
        }

        public ActionResult t_Khruphanths(string IDRL)
        {
            var AsData = _db.T_RequestList.Where(x => x.RL_RequisitionID == IDRL).ToList();
            var _data = new List<T_RequestList>(AsData);

            if (IDRL != null)
            {
                var chk = _db.T_Requisition.FirstOrDefault(x => x.RequisitionID == IDRL);
                var chk1 = _db.T_Requisition.Where(x => x.RequisitionID == IDRL).ToList();
                if (chk1.Count > 0)
                {
                    //chk.Re_StepID = "1";

                    var _Khruphanths = new List<T_Khruphanth>();

                    foreach (var data in _data)
                    {
                        string TmpCODE;
                        for (int i = 0; i < data.RL_Amount; i++)
                        {

                            TmpCODE = data.RL_CategoryID + "." + data.RL_TypeID + "." + (data.RL_OnStart + i) + "/" + chk.Re_DateRequi.Value.ToString("yy");
                            string code = TmpCODE;
                            QRCodeGenerator qrGenerator = new QRCodeGenerator();
                            //QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                            QRCodeData qrCodeData = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                            imgBarCode.Height = 250;
                            imgBarCode.Width = 250;
                            byte[] byteImage;
                            QRCode xx = new QRCode(qrCodeData);
                            using (Bitmap bitMap = xx.GetGraphic(20))
                            {
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                    byteImage = ms.ToArray();
                                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                    //string result = Convert.ToBase64String(byteImage, 0, byteImage.Length); ;
                                    //CreateImage(result);
                                }
                                //plQRCode.Controls.Add(imgBarCode);

                            }
                            //foreach (var item in phatQRs)
                            //{

                            //    Test = item.Path;
                            //}

                            _Khruphanths.Add(new T_Khruphanth { KhruphanthID = TmpCODE, Kh_RequestLsitID = data.RequestLsitID, Kh_StatusID = 1, Kh_PlaceID = data.RL_PlaceID, Kh_QR_CODE = byteImage });
                        }
                    }

                    _db.T_Khruphanth.AddRange(_Khruphanths);
                    if (chk != null)
                    {
                        chk.Re_StepID = "1";
                        _db.SaveChanges();
                        Mo(chk);
                    }

                    Session["Result"] = "okC";
                    return RedirectToAction("Index", "Khruphanths");
                }
                else
                {
                    Session["Result"] = "error0";
                    return RedirectToAction("Details", "Requisition");
                }
            }
            else
            {
                Session["Result"] = "error1";
                return RedirectToAction("Details", "Requisition");

            }
        }
        public void Mo(T_Requisition IDPOST)
        {
            _db.Entry(IDPOST).State = EntityState.Modified;
            _db.SaveChanges();
        }


        public string CreateImage(string Byt)
        {

            try
            {
                byte[] data = Convert.FromBase64String(Byt);
                var filename = Convert.ToString(Guid.NewGuid()).Substring(0, 5) + Convert.ToString(Guid.NewGuid()).Substring(0, 5) + DateTime.Now.ToString("FFFFFF") + DateTime.Now.Minute + ".png";// +System.DateTime.Now.ToString("fffffffffff") + ".png";
                var file = HttpContext.Server.MapPath("~/QR_CODE/" + filename);
                var Path = "~/QR_CODE/" + filename;
                PhatQRs.Add(new PhatQR { Path = Path });
                System.IO.File.WriteAllBytes(file, data);
                //string ImgName = ".../profileimages/" + filename;

                return filename;

            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }

        // GET: RequestList/Edit/5
        public ActionResult Edit(int id)
        {
            //List<T_Type> LstType = new List<T_Type>();
            ////LstType.Insert(0, new T_Type { TypeID = "0", TY_NameType = "----เลือกชนิด----" });
            ModelState.Remove("ImageUpload");
            var data = _db.T_RequestList.FirstOrDefault(a => a.RequestLsitID == id);

            if (data != null)
            {
                TempData["ID"] = data.RL_RequisitionID;

                ViewBag.RL_CategoryID = new SelectList(_db.T_Category, "CategoryID", "CA_NameCategory", data.RL_CategoryID);
                ViewBag.RL_TypeID = new SelectList(_db.T_Type, "TypeID", "TY_NameType", data.RL_TypeID);
                ViewBag.RL_PlaceID = new SelectList(_db.T_Place, "PlaceID", "PL_NamePlace", data.RL_PlaceID);

            }
            return View(data);
        }

        // POST: RequestList/Edit/5
        [HttpPost]
        public ActionResult Edit(T_RequestList data)
        {
            ViewBag.RL_CategoryID = new SelectList(_db.T_Category, "CategoryID", "CA_NameCategory", data.RL_CategoryID);
            ViewBag.RL_TypeID = new SelectList(_db.T_Type, "TypeID", "TY_NameType", data.RL_TypeID);
            ViewBag.RL_PlaceID = new SelectList(_db.T_Place, "PlaceID", "PL_NamePlace", data.RL_PlaceID);
            var value = _db.T_RequestList.Where(a =>a.RL_CategoryID == data.RL_CategoryID && a.RL_TypeID == data.RL_TypeID &&
                a.RL_RequisitionID != data.RL_RequisitionID).ToList();

            List<string> Tmp_CHK = new List<string>();
            List<string> Add_CHK = new List<string>();
            foreach (var item in value)
            {
                for (int i = 0; i < item.RL_Amount; i++)
                {
                    Tmp_CHK.Add(item.RL_CategoryID + item.RL_TypeID + i);
                }
            }

            for (int i = 0; i < data.RL_Amount; i++)
            {
                Add_CHK.Add(data.RL_CategoryID + data.RL_TypeID + i);
            }

            int P = 0;
            foreach (var Lopdb in Tmp_CHK)
            {
                foreach (var lopCn in Add_CHK)
                {
                    if (Lopdb == lopCn)
                    {
                        P++;
                    }
                }
            }
            if (data.RL_Amount < 1)
            {
                ModelState.AddModelError("RL_Amount", "กรุณากรอกข้อมูล ให้มากกว่า 0");
            }
            else if (data.RL_OnStart < 1)
            {
                ModelState.AddModelError("RL_OnStart", "กรุณากรอกข้อมูล ให้มากกว่า 0");
            }
            else if (data.RL_Price < 1)
            {
                ModelState.AddModelError("RL_Price", "กรุณากรอกข้อมูล ให้มากกว่า 0");
            }
            else
            {
                if (P == 0)
                {
                    if (data.ImageUpload != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(data.ImageUpload.FileName);
                        string extension = Path.GetExtension(data.ImageUpload.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        data.RL_PictureKhru = "~/Images/" + fileName;
                        data.ImageUpload.SaveAs(Path.Combine(Server.MapPath("~/Images/"), fileName));
                    }

                    if (ModelState.IsValid)
                    {
                        _db.Entry(data).State = EntityState.Modified;
                        _db.SaveChanges();
                        Session["Result"] = "okE";
                        return RedirectToAction("Details", "Requisition", new { RequisitionID = data.RL_RequisitionID });
                    }
                }

                ModelState.AddModelError("RL_OnStart", "ข้อมูลนี้มีอยู่ในฐานข้อมูลแล้ว  กรุณาตรวจสอบอีกครั้ง");
            }

            return View(data);

        }

        public ActionResult Delete(int id)
        {

            var data = _db.T_RequestList.FirstOrDefault(a => a.RequestLsitID == id);
            if (data != null)
            {
                _db.T_RequestList.Remove(data);
                _db.SaveChanges();
                Session["Result"] = "ok";
                return RedirectToAction("Details", "Requisition", new { RequisitionID = data.RL_RequisitionID });
            }

            return RedirectToAction("Waiting", "Requisition");
        }
    }
}
