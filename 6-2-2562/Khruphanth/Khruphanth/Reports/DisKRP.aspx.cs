using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Khruphanth.Models;
using Microsoft.Reporting.WebForms;

namespace Khruphanth.Reports
{
    public partial class DisKRP : System.Web.UI.Page
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

                var ViewR = db.View_DisKH.Where(a => a.Di_Status == "2").ToList();
                ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/R_Dsit.rdlc");
                ReportViewer1.LocalReport.DataSources.Clear();
                var rdc = new ReportDataSource("DataSet1", ViewR);
                ReportViewer1.LocalReport.DataSources.Add(rdc);
                ReportViewer1.LocalReport.Refresh();


            }

        }


        protected void Button1_OnClick(object sender, EventArgs e)
        {
            var date1 = inputdatepicker.Text;
            var date2 = inputdatepicker2.Text;
            var data = db.View_DisKH.ToList();
            if (!String.IsNullOrEmpty(date1)&& !string.IsNullOrEmpty(date2))
            {
                var d1 = Convert.ToDateTime(date1);
                var d2 = Convert.ToDateTime(date2);
                data = db.View_DisKH.Where(a => a.Di_Date >= d1 && a.Di_Date <= d2).ToList();
            }
            var rd = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/R_Dsit.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);
        }
        protected void Button2_OnClick(object sender, EventArgs e)
        {
            string textID = TextBox2.Text;
            var data = db.View_DisKH.ToList();
            if (!String.IsNullOrEmpty(textID))
            {
                 data = db.View_DisKH.Where(a => a.DL_DistributorID == textID).ToList();
            }
            var rd = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/R_Dsit.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);
        }


    }
}