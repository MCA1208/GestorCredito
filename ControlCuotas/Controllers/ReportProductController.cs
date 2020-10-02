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
    public class ReportProductController : Controller
    {
        public string userNameLogin = "";
        public int userIdLogin;
        public int userIdProfile;
        public ReportProductController()
        {
            userNameLogin = System.Web.HttpContext.Current.Session["userName"]?.ToString();
            userIdLogin = System.Web.HttpContext.Current.Session["idUser"] != null ? (int)System.Web.HttpContext.Current.Session["idUser"] : 0;
            userIdProfile = System.Web.HttpContext.Current.Session["idProfile"] != null ? (int)System.Web.HttpContext.Current.Session["idProfile"] : 0;
        }

        DataTable dt = null;
        public int idUser = 0;
        ResultModel data = new ResultModel();
        Service.ReportProductService Service = new Service.ReportProductService();
        // GET: ReportProduct
        public ActionResult ReportCuponProductClient()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Login");

            return View();
        }

        
        public JsonResult GetReportProductCuponClient(int IdClient)
        {
            try
            {
                dt = Service.GetReportCuponClient(IdClient);

                if (dt.Rows.Count == 0)
                {
                    data.message = "Búsqueda sin resultados";
                    data.status = "error";

                }

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
    }
}