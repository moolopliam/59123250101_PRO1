using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Khruphanth.Models;
namespace Khruphanth.Reports
{
    public partial class RemainKH : System.Web.UI.Page
    {
        private readonly ComCSDBEntities db = new ComCSDBEntities();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["CHKNAME"] == null)
            {
                HttpContext.Current.Response.Redirect("~/Home/Login");
            }
            if (!IsPostBack)
            {

                var ViewR = db.View_RemainRT_KH.Where(a => a.Re_StepID == "1").ToList();

                foreach (var itKh in ViewR)
                {
                        var KP = db.T_Khruphanth.Where(a => a.Kh_RequestLsitID == itKh.RequestLsitID && a.Kh_StatusID == 2).ToList();
                        itKh.RL_Amount -= KP.Count;
                    
                }

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RemainKH.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                var rdc = new ReportDataSource("DataSet1", ViewR);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.Refresh();


            }

        }
        protected void Button4_Click4(object sender, EventArgs e)
        {

            var ViewR = db.View_RemainRT_KH.Where(a => a.Re_StepID == "1").ToList();

            foreach (var itKh in ViewR)
            {
                var KP = db.T_Khruphanth.Where(a => a.Kh_RequestLsitID == itKh.RequestLsitID && a.Kh_StatusID == 2).ToList();
                itKh.RL_Amount -= KP.Count;

            }
            var rd = new ReportDataSource("DataSet1", ViewR);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RemainKH.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);
        }

        protected void Button9_Click9(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("~/Home/Index");
        }

        protected void Button2_Click2(object sender, EventArgs e)
        {
            var t1 = TextBox2.Text;
            var ViewR = db.View_RemainRT_KH.Where(a => a.Re_StepID == "1").ToList();

            foreach (var itKh in ViewR)
            {
                var KP = db.T_Khruphanth.Where(a => a.Kh_RequestLsitID == itKh.RequestLsitID && a.Kh_StatusID == 2).ToList();
                itKh.RL_Amount -= KP.Count;

            }
            if (!String.IsNullOrEmpty(t1))
            {
                ViewR = db.View_RemainRT_KH.
                    Where(p => p.RequisitionID.Contains(t1)).ToList();


            }
            var rd = new ReportDataSource("DataSet1", ViewR);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RemainKH.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);
        }

        protected void Button7_Click7(object sender, EventArgs e)
        {
            var t1 = DropDownList1.Text;
            var ViewR = db.View_RemainRT_KH.Where(a => a.Re_StepID == "1").ToList();

            foreach (var itKh in ViewR)
            {
                var KP = db.T_Khruphanth.Where(a => a.Kh_RequestLsitID == itKh.RequestLsitID && a.Kh_StatusID == 2).ToList();
                itKh.RL_Amount -= KP.Count;

            }
            if (!String.IsNullOrEmpty(t1))
            {
                ViewR = db.View_RemainRT_KH.
                    Where(p => p.TeaName == t1).ToList();


            }
            var rd = new ReportDataSource("DataSet1", ViewR);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/RemainKH.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);
        }
    }
}