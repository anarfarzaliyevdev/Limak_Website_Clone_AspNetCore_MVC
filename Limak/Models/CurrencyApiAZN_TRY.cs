using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Models
{
    public class CurrencyApiAZN_TRY
    {
        public bool status { get; set; }
        public int code { get; set; }
        public string msg { get; set; }
        public ResponseAZN_TRY response { get; set; }
        public Info info { get; set; }
    }

    public class ResponseAZN_TRY
    {
        public string price_1x_AZN { get; set; }
        public string price_1x_TRY { get; set; }
        public string total { get; set; }
    }

    public class InfoAZN_TRY
    {
        public string server_time { get; set; }
        public int credit_count { get; set; }
        public string _t { get; set; }
    }

}
