using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlCuotas.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult ReportPrincipal()
        {
            return View();
        }
    }
}