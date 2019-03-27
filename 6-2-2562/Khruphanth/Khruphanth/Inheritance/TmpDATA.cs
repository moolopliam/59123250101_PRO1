using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Khruphanth.Inheritance
{
    public class TmpDATA
    {
        //[Required(ErrorMessage = "กรุณากรอกชื่อผู้เบิก")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime TMPdate { get; set; }
    }
    public class PIC
    {
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}