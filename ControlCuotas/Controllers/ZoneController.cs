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
    public class ZoneController : Controller
    {


        DataTable dt = null;
        public int idUser = 0;
        ResultModel data = new ResultModel();
        Service.ZoneService Service = new Service.ZoneService();


        // GET: Zone
        public ActionResult Principal()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Login");

            return View();
        }

        
        public JsonResult GetAllZone()
        {
            try
            {
                idUser = (int)System.Web.HttpContext.Current.Session["idUser"];


                dt = Service.GetAllZone();

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

        public JsonResult AddZone(string description)
        {
            try
            {
                dt = Service.AddZone( description);

                if ((int)dt.Rows[0][0] == 0)
                {
                    data.status = "error";
                    data.message = "error al agregar el cliente";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

                data.message = "Se agrego correctamente";
                data.result = JsonConvert.SerializeObject(data, Formatting.Indented);
            }
            catch (Exception ex)
            {
                data.message = ex.Message;
                data.status = "error";
                return Json(data, JsonRequestBehavior.AllowGet);

            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        
        public JsonResult GetZoneById(int IdZone)
        {
            try
            {
                dt = Service.GetZoneById(IdZone);

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

        public JsonResult ModifyZone(int IdZone, string Description)
        {
            try
            {
                dt = Service.ModifyZone(IdZone, Description);

                data.result = dt.Rows[0][0];

                if ((int)dt.Rows[0][0] == 1)
                {
                    data.message = "Se modificó correctamente";
                }
                else
                {
                    data.message = "No se pudo modificar correctamente";
                }
               
                data.result = JsonConvert.SerializeObject(data, Formatting.Indented);
            }
            catch (Exception ex)
            {
                data.message = ex.Message;
                data.status = "error";
                return Json(data, JsonRequestBehavior.AllowGet);

            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //End Class
    }
}