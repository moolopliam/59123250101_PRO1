<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QRCODE.aspx.cs" Inherits="Khruphanth.Reports.QRCODE" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ปริ้นQR_CODE</title>
    <link href="https://fonts.googleapis.com/css?family=Sriracha&amp;subset=thai" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Sriracha" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />

    <style>
        body {
            font-family: 'Sriracha', cursive;
        }
        /* ใช้เฉพาะหัวข้อ */
        h1, h2, h3, h4, h5, h6, p {
            font-family: 'Sriracha', cursive;
        }

        title {
            font-family: 'Sriracha', cursive;
        }

        .tk {
            width: 150px;
            height: 50px;
        }

        .form-DD {
            height: 43px !important;
            width: 150px !important;
            font-size: 14px !important;
            font-family: 'Sriracha', cursive;
        }

        .form-DD2 {
            height: 43px !important;
            width: 300px !important;
            font-size: 14px !important;
            font-family: 'Sriracha', cursive;
        }

        .form-DD1 {
            height: 43px !important;
            width: 150px !important;
            font-size: 14px !important;
            font-family: 'Sriracha', cursive;
        }

        .form-D2 {
            height: 43px !important;
            width: 200px !important;
            font-size: 14px !important;
            font-family: 'Sriracha', cursive;
            text-align: center;
        }


        .tes {
            height: 103px !important;
            /*width: 150px !important;*/
            font-size: 14px !important;
            font-family: 'Sriracha', cursive;
            border-radius: 30px;
        }

        .font {
            font-size: 14px !important;
            font-family: 'Sriracha', cursive;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <br />
        <div class="container">
            <h3>ค้นหาข้อมูลบาร์โค้ด</h3>
            <div class="row ">
            <div class="col-md-1">
                    <br/>
                    <asp:Button ID="Button4" runat="server" Text="ดูข้อมูลเดิม" class="btn-rounded btn btn-primary" OnClick="Button4_Click4" />
                </div>
                <%--          <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" Text="ค้นหาตามเลขครุภัณ์"></asp:Label>
                    <asp:TextBox ID="TextBox2" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <br/>
                    <asp:Button ID="Button3" runat="server" Text="ค้นหาข้อมูล" class="btn-rounded btn btn-primary" OnClick="Button2_Click2" />
                </div>--%>
                <div class="col-md-2">
                    <asp:Label ID="Label4" runat="server" Text="ค้นหาตามเลขที่ใบเบิก"></asp:Label>
                    <asp:TextBox ID="TextBox4" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <br/>
                    <asp:Button ID="Button7" runat="server" Text="ค้นหาข้อมูล" class="btn-rounded btn btn-primary" OnClick="Button7_Click7" />
                </div>
<%--                <div class="col-md-2">
                    <asp:Label ID="Label5" runat="server" Text="ค้นหาตามสถานที่"></asp:Label>
                    <asp:TextBox ID="TextBox5" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <br />
                    <asp:Button ID="Button5" runat="server" Text="ค้นหาข้อมูล" class="btn-rounded btn btn-primary" OnClick="Button5_Click5" />
                </div>--%>
                <div class="col-md-1">
                    <br />
                    <asp:Button ID="Button9" runat="server" Text="ย้อนกลับ" class="btn-rounded btn btn-info" OnClick="Button9_Click9" />
                </div>
            </div>
        </div>
        <br />
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" SizeToReportContent="True" Width="100%" ZoomMode="FullPage" fullScreenMode="true">
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
