using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Khruphanth.Models
{
    public class JoinTableRlKr
    {
        public string KhruphanthID { get; set; }
        public string Kh_QR_CODE { get; set; }
        public string Kh_Place { get; set; }
        public string Kh_Status { get; set; }
        public string RL_TypeID { get; set; }
        public string RL_CategoryID { get; set; }
        public string RL_NameKP { get; set; }
        public string RL_OnStart { get; set; }
        public string RL_Amount { get; set; }
        public string RL_Price { get; set; }
        public string RL_DetailKhru { get; set; }
        public string RL_PictureKhru { get; set; }
        public string RequisitionID { get; set; }
        public string Re_TeaId { get; set; }
        public string Re_DateRequi { get; set; }
        public string Re_StepID { get; set; }
    }
}