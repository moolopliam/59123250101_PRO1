using System.Linq;
using System.Web.Mvc;
using Khruphanth.Models;

namespace Khruphanth.Controllers
{
    public class HomeController : Controller
    {
        private readonly ComCSDBEntities _db = new ComCSDBEntities();
        public ActionResult Index()
        {
            var Price = _db.T_RequestList.ToList();
            var data = _db.T_Khruphanth.ToList();
            var TotalA = Price.Sum(a => a.RL_Amount);
            var TotalPrice = Price.Sum(x => x.RL_Price);
            TempData["ALO"] = data.Count(a => a.Kh_StatusID == 1);
            TempData["ALI"] = data.Count(a => a.Kh_StatusID == 2);
            TempData["ALX"] = TotalA * TotalPrice;
            TempData["ALC"] = _db.Teacher.Count();
            return View();
        }
       
        public ActionResult ReportRequisition()
        {
            return View();

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Details(string RequisitionID)
        {
            var data = _db.T_Khruphanth.FirstOrDefault(x => x.KhruphanthID == RequisitionID);
            return View(data);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Report()
        {
            return Redirect("~/Reports/ViewRequisition.aspx");
        }
        public ActionResult Report1()
        {
            return Redirect("~/Reports/QRCODE.aspx");
        }
        public ActionResult Report2()
        {
            return Redirect("~/Reports/Dis.aspx");
        }
        public ActionResult Report3()
        {
            return Redirect("~/Reports/RemainKH.aspx");
        }
        public ActionResult Login()
        {
            Session["CHKNAME"] = null;
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account data)
        {
            var chk = _db.Account.FirstOrDefault(a => a.AUsername == data.AUsername);
            var chkpass = _db.Account.FirstOrDefault(s => s.APassword == data.APassword);
            if(chk == null)
            {
                ModelState.AddModelError("AUsername", "กรุณาตรวจสอบ username");
                return View(data);
            }
            else if(chkpass == null)
            {
                ModelState.AddModelError("APassword", "กรุณาตรวจสอบ username");
                return View(data);
            }
            else
            {
                Session["CHKNAME"] = 1;
                return RedirectToAction(nameof(Index));
            }
           
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction(nameof(Login));
        }
    }
}