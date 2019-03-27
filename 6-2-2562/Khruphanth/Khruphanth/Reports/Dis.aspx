<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dis.aspx.cs" Inherits="Khruphanth.Reports.Dis" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>รายงานการจำหน่าย</title>
    <script src="../Scripts/jquery-3.3.1.min.js"></script>
    <link href="https://fonts.googleapis.com/css?family=Sriracha&amp;subset=thai" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Sriracha" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <link href="../Content/oi/dist/css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../Content/oi/dist/js/bootstrap-datepicker-custom.js"></script>
    <script src="../Content/oi/dist/locales/bootstrap-datepicker.th.min.js"></script>

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
    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker({
                format: 'dd/mm/yyyy',
                todayBtn: true,
                language: 'th',             //เปลี่ยน label ต่างของ ปฏิทิน ให้เป็น ภาษาไทย   (ต้องใช้ไฟล์ bootstrap-datepicker.th.min.js นี้ด้วย)
                thaiyear: true              //Set เป็นปี พ.ศ.
            }).datepicker("setDate", "0");  //กำหนดเป็นวันปัจุบัน
        });
    </script>
    <form id="form1" runat="server">
        <div class="container">
            <br />
            <h3>ค้นหาข้อมูล</h3>
            <div class="row">
                <div class="col-md-2">
                  <br/>
                <asp:Button ID="Button5" runat="server" Text="ดูข้อมูลเดิม" class="btn-rounded btn btn-primary" OnClick="Button5_Click5" />
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label1" runat="server" Text="ค้นหาตามวันที่ วัน/เดือน/ปี " ></asp:Label>
                    <asp:TextBox ID="inputdatepicker" runat="server" class="form-control datepicker" data-date-format="mm/dd/yyyy"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <asp:Label runat="server" Text="ค้นหาตามวันที่ วัน/เดือน/ปี " ></asp:Label>
                    <asp:TextBox ID="TextBox4" runat="server" class="form-control datepicker" data-date-format="mm/dd/yyyy"></asp:TextBox>
                </div>
                <div class="col-md-2">
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="ค้นหาตามวันที่" class="btn-rounded btn btn-primary" OnClick="Button1_Click1" />
                </div>
                <div class="col-md-2">
                    <asp:Label ID="Label3" runat="server" Text="ค้นหาตามเลขครุภัณ์"></asp:Label>
                    <asp:TextBox ID="TextBox2" class="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <br />
                    <asp:Button ID="Button3" runat="server" Text="ค้นหาข้อมูล" class="btn-rounded btn btn-primary" OnClick="Button2_Click2" />
                </div>
                <div class="col-md-1">
                    <br />
                    <asp:Button ID="Button7" runat="server" Text="ย้อนกลับ" class="btn-rounded btn btn-info" OnClick="Button7_Click7" />
                </div>
            </div>
            <br />
        </div>
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" SizeToReportContent="True" Width="100%" ZoomMode="FullPage" fullScreenMode="true">
            </rsweb:ReportViewer>
        </div>
    </form>
</body>
</html>
