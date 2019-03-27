using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Khruphanth.Models
{
    public partial class T_Requisition_V
    {

        [Display(Name = "ใบเบิกที่")]
        [Required(ErrorMessage = "กรุณากรอกใบที่เบิก")]
        public string RequisitionID { get; set; }

        [Display(Name = "ผู้เบิก")]
        [Required(ErrorMessage = "กรุณาเลือกผู้เบิก")]
        public string Re_TeaId { get; set; }

        [Display(Name = "วันที่เบิก")]
        [Required(ErrorMessage = "กรุณากรอกวันที่เบิก")]
        public Nullable<System.DateTime> Re_DateRequi { get; set; }

        [Display(Name = "สถานะการเบิก")]
        public string Re_StepID { get; set; }

    }
    [MetadataType(typeof(T_Requisition_V))]
    public partial class T_Requisition { }

    public partial class T_RequestList_VA
    {

        [Display(Name = "เลขที่รายการเบิก")]
        public int RequestLsitID { get; set; }
        [Display(Name = "ใบเบิกที่")]
        [Required(ErrorMessage = "กรุณากรอกใบเบิกที่")]
        public string RL_RequisitionID { get; set; }
        [Display(Name = "ประเภท")]
        [Required(ErrorMessage = "กรุณาเลือกประเภท")]
        public string RL_TypeID { get; set; }
        [Display(Name = "หมวด")]
        [Required(ErrorMessage = " กรุณาเลือกหมวด")]
        public string RL_CategoryID { get; set; }
        [Display(Name = "ชื่ออุปกรณ์")]
        [MinLength(2,ErrorMessage = "กรุณากรอกข้อมูลมากกว่า 2 ตัวอักษร")]
        [MaxLength(50,ErrorMessage = "กรุณากรอกข้อมูลน้อยกว่า 50 ตัวอักษร")]
        [Required(ErrorMessage = "กรุณากรอกชื่ออุปกรณ์")]
        public string RL_NameKP { get; set; }
        [Display(Name = "เลขเริ่มที่")]
        [Required(ErrorMessage = "กรุณากรอกเลขเริ่มที่")]
        [Range(0, 10000, ErrorMessage = "กรุณาใส่จำนวน {1} ถึง {2}")]
        [RegularExpression(@"^\d{1,13}$", ErrorMessage = "ใส่ข้อมูลเป็นตัวเลขเท่านั้น")]
        public Nullable<int> RL_OnStart { get; set; }
        [Display(Name = "จำนวน")]
        [Required(ErrorMessage = "กรุณากรอกจำนวน")]
        //[Range(Int32.MinValue, 10)]
        //[Range(1Int32.MinValue ,100000, ErrorMessage = "กรอกเกินจำนวน")]
        [Range(0, 10000, ErrorMessage = "กรุณาใส่จำนวน {1} ถึง {2}")]
        [RegularExpression(@"^\d{1,13}$", ErrorMessage = "ใส่ข้อมูลเป็นตัวเลขเท่านั้น")]
        public Nullable<double> RL_Amount { get; set; }
        [Display(Name = "ราคาต่อหน่วย")]
        [Range(0, 10000000, ErrorMessage = "กรุณาใส่จำนวน {1} ถึง {2}")]
        [RegularExpression(@"^\d{1,15}$", ErrorMessage = "ใส่ข้อมูลเป็นตัวเลขเท่านั้น")]
        [Required(ErrorMessage = "กรุณากรอกราคาต่อหน่วย")]
        public Nullable<double> RL_Price { get; set; }
        [Display(Name = "รายละเอียดครุภัณฑ์")]
        [MinLength(2, ErrorMessage = "กรุณากรอกข้อมูลมากกว่า 2 ตัวอักษร")]
        [Required(ErrorMessage = "กรุณากรอกรายละเอียดของครุภัณฑ์")]
        public string RL_DetailKhru { get; set; }
        [Display(Name = "รูปภาพ")]
        //[Required(ErrorMessage = "กรุณาเลือกรูปภาพ")]
        public string RL_PictureKhru { get; set; }
        [Display(Name = "สถานที่เก็บครุภัณฑ์")]
        [Required(ErrorMessage = "กรุณากรอกสถานที่เก็บครุภัณฑ์")]
        public Nullable<int> RL_PlaceID { get; set; }


    }
    [MetadataType(typeof(T_RequestList_VA))]
    public partial class T_RequestList { }


    public partial class T_Category_VA
    {
        [Display(Name = "รหัสหมวด")]
        [Required(ErrorMessage = "รหัสหมวด")]
        public string CategoryID { get; set; }
        [Display(Name = "ชื่อหมวด")]
        [Required(ErrorMessage = "กรุณากรอกชื่อหมวด")]
        public string CA_NameCategory { get; set; }

    }
    [MetadataType(typeof(T_Category_VA))]
    public partial class T_Category { }


    public partial class T_DistributorV
    {
        [Display(Name = "ใบจำหน่ายที่")]
        [Required(ErrorMessage = "กรุณากรอกเลขใบจำหน่าย")]
        //[RegularExpression("([0-9]+)", ErrorMessage = "กรุณาใส่เป็นตัวเลข")]
        public string DistributorID { get; set; }
        [Display(Name = "วันที่จำหน่าย")]
        public Nullable<System.DateTime> Di_Date { get; set; }
        [Display(Name = "สถานะ")]
        public string Di_Status { get; set; }

    }
    [MetadataType(typeof(T_DistributorV))]
    public partial class T_Distributor { }

    public partial class T_DistributorList_VA
    {
        [Display(Name = "รหัสรายการจำหน่าย")]
        public int DistributorList { get; set; }
        [Display(Name = "ใบจำหน่ายที่")]
        [Required(ErrorMessage = "กรุณากรอกเลขใบจำหน่าย")]
        public string DL_DistributorID { get; set; }
        [Display(Name = "ครุภัณฑ์")]
        [Required(ErrorMessage = "กรุณากรอกครุภัณฑ์")]
        public string DL_KhruphanthID { get; set; }

    }
    [MetadataType(typeof(T_DistributorList_VA))]
    public partial class T_DistributorList { }


    public partial class T_Place_VA
    {

        [Display(Name = "รหัสที่เก็บของ")]
        public int PlaceID { get; set; }
        [Display(Name = "ที่เก็บของ")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string PL_NamePlace { get; set; }

    }
    [MetadataType(typeof(T_Place_VA))]
    public partial class T_Place { }

    public partial class T_Type_VA
    {
        [Display(Name = "รหัสชนิด")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string TypeID { get; set; }
        [Display(Name = "ชื่อชนิด")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string TY_NameType { get; set; }
        [Display(Name = "หมวด")]
        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string TY_CategoryID { get; set; }

    }
    [MetadataType(typeof(T_Type_VA))]
    public partial class T_Type { }


    public partial class T_Khruphanth_VA
    {
        [Display(Name = "รหัสครุภัณฑ์")]
        public string KhruphanthID { get; set; }
        [Display(Name = "รหัสรายการเบิก")]
        public Nullable<int> Kh_RequestLsitID { get; set; }
        public byte[] Kh_QR_CODE { get; set; }
        [Display(Name = "สถานที่เก็บ")]
        public Nullable<int> Kh_PlaceID { get; set; }
        [Display(Name = "สถานะ")]
        public Nullable<int> Kh_StatusID { get; set; }

    }
    [MetadataType(typeof(T_Khruphanth_VA))]
    public partial class T_Khruphanth { }
}