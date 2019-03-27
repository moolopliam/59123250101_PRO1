using Khruphanth.Models;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Khruphanth.Reports
{
    public partial class Dis : System.Web.UI.Page
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
                List<View_Distributor> ViewR = null;
   
                using (ComCSDBEntities dc = new ComCSDBEntities())
                {
                    ViewR = dc.View_Distributor.OrderBy(a => a.DistributorID).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report3.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", ViewR);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();

                }
            }

        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            var t2 = TextBox4.Text;
            var t1 = inputdatepicker.Text;
            var data = db.View_Distributor.OrderBy(p => p.DistributorID).ToList();
            if (Convert.ToDateTime(t2) != DateTime.Now.Date)
            {
                var date = Convert.ToDateTime(t1);
                var date2 = Convert.ToDateTime(t2);
                data = db.View_Distributor.Where(a => a.Di_Date >= date && a.Di_Date <= date2 ).ToList();
            }
            else
            {
                var date = Convert.ToDateTime(t1);
                data = db.View_Distributor.Where(a => a.Di_Date == date ).ToList();
            }
            var rd = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report3.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);

        }

        protected void Button2_Click2(object sender, EventArgs e)
        {
            var t1 = TextBox2.Text;
            var data = db.View_Distributor.OrderBy(p => p.DistributorID).ToList();
            if (!String.IsNullOrEmpty(t1))
            {
                data = db.View_Distributor.Where(p => p.DistributorID.Contains(t1)).ToList();

            }
            var rd = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report3.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);
        }

        protected void Button5_Click5(object sender, EventArgs e)
        {
            var data = db.View_Distributor.OrderBy(p => p.DistributorID).ToList();
            var rd = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report3.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);
        }

        protected void Button7_Click7(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("~/Home/Index");
        }
    }
}