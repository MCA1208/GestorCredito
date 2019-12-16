using ControlCuotas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlCuotas.Controllers
{
    public class ReportController : Controller
    {

        DataTable dt = null;
        public int idUser = 0;
        ResultModel data = new ResultModel();
        Service.ReportService Service = new Service.ReportService();
        // GET: Report
        public ActionResult ReportPrincipal()
        {
            return View();
        }


        public JsonResult GetReportPrincipal(int? IdClient, int? IdZone, DateTime? dateFrom, DateTime? DateUp)
        {
            try
            {

                dt = Service.GetReportPrincipal(IdClient, IdZone, dateFrom, DateUp);



                data.result = JsonConvert.SerializeObject(dt, Formatting.Indented);
            }
            catch (Exception ex)
            {
                data.message = ex.Message;
                data.status = "error";
                return Json(data, JsonRequestBehavior.AllowGet);

            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }//Fin Class
}