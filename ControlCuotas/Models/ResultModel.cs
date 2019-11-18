using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlCuotas.Models
{
    public class ResultModel
    {
        public object result { get; set; } = "";

        public string status { get; set; } = "ok";

        public string message { get; set; }

        public string url { get; set; }
    }
}