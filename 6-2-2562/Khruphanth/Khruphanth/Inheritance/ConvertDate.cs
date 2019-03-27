using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Khruphanth.Inheritance
{
    public class ConvertDate
    {

        public string ThaiDate(DateTime? Mydate)
        {
            if (Mydate == null) return null;

            string userDate = String.Format("{0:dd/MM/yyyy}", Mydate);

            var cultureEN = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");
            DateTime dt = DateTime.ParseExact(userDate, "dd/MM/yyyy", cultureEN);

            var cultureTH = System.Globalization.CultureInfo.CreateSpecificCulture("th-TH");

            string thaidate = dt.ToString("dd/MM/yyyy", cultureTH);

            return thaidate;
        }

        //public string EngDate1(string Mydate)
        //{
        //    if (String.IsNullOrEmpty(Mydate)) return null;

        //    string userDateTH = Mydate;

        //    var cultureEN = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

        //    var cultureTH = System.Globalization.CultureInfo.CreateSpecificCulture("th-TH");
        //    DateTime dtTH = DateTime.ParseExact(userDateTH, "dd/MM/yyyy", cultureTH);

        //    string engdate = dtTH.ToString("dd/MM/yyyy", cultureEN);

        //    return engdate;
        //}

        public DateTime EngDate(string Mydate)
        {
            if (String.IsNullOrEmpty(Mydate)) return DateTime.Now;

            string userDateTH = Mydate;

            var cultureEN = System.Globalization.CultureInfo.CreateSpecificCulture("en-US");

            var cultureTH = System.Globalization.CultureInfo.CreateSpecificCulture("th-TH");
            DateTime dtTH = DateTime.ParseExact(userDateTH, "dd/MM/yyyy", cultureTH);

            string engdate = dtTH.ToString("dd/MM/yyyy", cultureEN);

            return dtTH;
        }

    }
}