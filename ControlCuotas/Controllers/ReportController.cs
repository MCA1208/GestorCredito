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
        public string userNameLogin = "";
        public int userIdLogin;
        public int userIdProfile;
        public ReportController()
        {
            userNameLogin = System.Web.HttpContext.Current.Session["userName"]?.ToString();
            userIdLogin = System.Web.HttpContext.Current.Session["idUser"] != null ? (int)System.Web.HttpContext.Current.Session["idUser"] : 0;
            userIdProfile = System.Web.HttpContext.Current.Session["idProfile"] != null ? (int)System.Web.HttpContext.Current.Session["idProfile"] : 0;
        }

        DataTable dt = null;
        public int idUser = 0;
        ResultModel data = new ResultModel();
        Service.ReportService Service = new Service.ReportService();
        // GET: Report
        public ActionResult ReportPrincipal()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Login");

            return View();
        }

        public ActionResult ReportCuponStatus()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Home");

            return View();
        }

        public ActionResult ReportInvestmentAndProfit()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Login");

            return View();
        }
        
        public ActionResult ReportCuponClient()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Login");

            return View();
        }

        public ActionResult ReportSummaryClient()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Login");

            return View();
        }

        public ActionResult ReportCobranza()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Login");

            return View();
        }

        public ActionResult ReportIrregularPayment()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Login");

            return View();
        }

        public JsonResult GetReportPrincipal(int? IdClient, int? IdZone, DateTime? dateFrom, DateTime? DateUp)
        {
            try
            {
                dt = Service.GetReportPrincipal(IdClient, IdZone, dateFrom, DateUp, userIdLogin, userIdProfile);
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


        public JsonResult GetReportCuotaStatus(int? IdClient, int? IdZone, DateTime? DateStart, DateTime? DateEnd)
        {
            try
            {
                dt = Service.GetReportCuotaStatus(IdClient, IdZone, DateStart, DateEnd, userIdLogin, userIdProfile);
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

        public JsonResult GetReportGanancia(DateTime? DateStart, DateTime? DateEnd)
        {
            try
            {
                dt = Service.GetReportGanancia(DateStart, DateEnd, userIdLogin, userIdProfile);
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
        

       public JsonResult GetReportCuponClient(int IdClient)
        {
            try
            {
                dt = Service.GetReportCuponClient(IdClient);

                if(dt.Rows.Count == 0)
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

        public JsonResult GetReportSummaryClient(int IdClient)
        {
            try
            {
                dt = Service.GetReportSummaryClient(IdClient, userIdLogin, userIdProfile);

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

        public JsonResult GetReportSummaryDetail(int IdPrest)
        {
            try
            {
                dt = Service.GetReportSummaryDetail(IdPrest);

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
       
        public JsonResult GetReportCobranza(int IdZone)
        {
            try
            {
                dt = Service.GetReportCobranza(IdZone, userIdLogin, userIdProfile);

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

        public JsonResult GetReportIrregularPayment(int? IdClient, int? IdZone)
        {
            try
            {
                dt = Service.GetReportIrregularPayment(IdClient, IdZone);

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

    }//Fin Class 
}