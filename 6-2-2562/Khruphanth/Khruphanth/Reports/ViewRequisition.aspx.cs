using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Khruphanth.Models;
using Microsoft.Reporting.WebForms;

namespace Khruphanth.Reports
{
    public partial class ViewRequisition : System.Web.UI.Page
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
                List<View_Requisition> ViewR = null;
                using (ComCSDBEntities dc = new ComCSDBEntities())
                {
                    ViewR = dc.View_Requisition.OrderBy(a => a.TeaName).ToList();
                    ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_Requisittion.rdlc");
                    ReportViewer1.LocalReport.DataSources.Clear();
                    ReportDataSource rdc = new ReportDataSource("DataSet1", ViewR);
                    ReportViewer1.LocalReport.DataSources.Add(rdc);
                    ReportViewer1.LocalReport.Refresh();

                }
            }

        }
        protected void Button1_Click1(object sender, EventArgs e)
        {
            var t2 = DropDownList1.SelectedValue;
            var t1 = inputdatepicker.Text;
            var t3 = TextBox5.Text;
            var data = db.View_Requisition.OrderBy(a => a.TeaName).ToList();
            if (!String.IsNullOrEmpty(t2)&& !String.IsNullOrEmpty(t1)&&!String.IsNullOrEmpty(t3))
            {
                var dt1 = Convert.ToDateTime(t1);
                var dt3 = Convert.ToDateTime(t3);
                //var chkdate = DateTime.Now.Date.ToString("dd/MM/yyyy");
                if (Convert.ToDateTime(t1) != DateTime.Now.Date && Convert.ToDateTime(t3) != DateTime.Now.Date)
                {
                    data = db.View_Requisition.
                        Where(p => p.Re_DateRequi >= dt1 && p.Re_DateRequi <= dt3 && p.TeaName == t2).ToList();
                }
                else if (Convert.ToDateTime(t1) != DateTime.Now.Date)
                {
                    data = db.View_Requisition.
                        Where(p => p.Re_DateRequi == dt1 && p.TeaName == t2).ToList();
                }
                else
                {
                    data = db.View_Requisition.
                        Where(p => p.TeaName == t2).ToList();
                }
                //var s1 = Convert.ToInt32(t2);


            }
            var rd = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_Requisittion.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);
            DropDownList1.ClearSelection();
            inputdatepicker.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        }

        protected void Button2_Click2(object sender, EventArgs e)
        {
            var t1 = TextBox2.Text;
            var data = db.View_Requisition.OrderBy(p => p.TeaName).ToList();
            if (!String.IsNullOrEmpty(t1))
            {
                    data = db.View_Requisition.
                 Where(p => p.RequisitionID.Contains(t1)).ToList();
                

            }
            var rd = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_Requisittion.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);
        }

        protected void Button7_Click7(object sender, EventArgs e)
        {

            var data = db.View_Requisition.OrderBy(p => p.TeaName).ToList();
            var rd = new ReportDataSource("DataSet1", data);
            ReportViewer1.LocalReport.ReportPath = Server.MapPath("~/Reports/Report_Requisittion.rdlc");
            ReportViewer1.LocalReport.DataSources.Clear();
            ReportViewer1.LocalReport.DataSources.Add(rd);
        }

        protected void Button6_Click6(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect("~/Home/Index");
        }
    }
}