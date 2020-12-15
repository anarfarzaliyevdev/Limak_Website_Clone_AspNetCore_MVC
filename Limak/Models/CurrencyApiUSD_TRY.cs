using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Limak.Models
{
    public class CurrencyApiUSD_TRY
    {
        public bool status { get; set; }
        public int code { get; set; }
        public string msg { get; set; }
        public Response[] response { get; set; }
        public Info info { get; set; }
    }

    public class Info
    {
        public string server_time { get; set; }
        public int credit_count { get; set; }
    }

    public class Response
    {
        public string id { get; set; }
        public string price { get; set; }
        public string change { get; set; }
        public string chg_per { get; set; }
        public string last_changed { get; set; }
        public string symbol { get; set; }
    }
}