using System.Linq;
using System.Web.Mvc;
using Khruphanth.Models;
namespace Khruphanth.Controllers
{
    public class KhruphanthsController : Controller
    {
 
        private readonly ComCSDBEntities _db = new ComCSDBEntities();
        public ActionResult Index()
        {
            var data = _db.T_Khruphanth.ToList();
            return View(data);
        }


        public ActionResult Details(string RequisitionID)
        {
            var data = _db.T_Khruphanth.FirstOrDefault(x => x.KhruphanthID == RequisitionID);
            return View(data);
        }

    }
}
