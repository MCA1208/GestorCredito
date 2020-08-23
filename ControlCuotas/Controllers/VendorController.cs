using ControlCuotas.Models;
using ControlCuotas.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlCuotas.Controllers
{
    public class VendorController : Controller
    {
        // GET: Vendor
        public string userNameLogin = "";
        public int userIdLogin;
        public int userIdProfile;
        DataTable dt = null;
        public int idUser = 0;
        ResultModel data = new ResultModel();
        VendorService service = new VendorService();
        string jsonString = string.Empty;
        public VendorController()
        {
            userNameLogin = System.Web.HttpContext.Current.Session["userName"]?.ToString();
            userIdLogin = System.Web.HttpContext.Current.Session["idUser"] != null ? (int)System.Web.HttpContext.Current.Session["idUser"] : 0;
            userIdProfile = System.Web.HttpContext.Current.Session["idProfile"] != null ? (int)System.Web.HttpContext.Current.Session["idProfile"] : 0;
        }
        public ActionResult Principal()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Login");

            return View();
        }

        public JsonResult AddVendor(string name, int dni, DateTime birthDay )
        {
            try
            {
                dt = service.AddVendor(name, dni, birthDay,userNameLogin, userIdLogin);
                data.result = JsonConvert.SerializeObject(dt, Formatting.Indented);
                data.message = "Se creó el vendedor con éxito";
              
            }
            catch (Exception ex)
            {
                data.message = ex.Message;
                data.status = "error";
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllVendor()
        {
            try
            {
                dt = service.GetAllVendor(userIdLogin, userIdProfile);
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

        public JsonResult ModifyVendor(int IdVendor, string name, int dni, DateTime birthday)
        {

            try
            {
                dt = service.ModifyVendor(IdVendor, name, dni, birthday, userNameLogin, userIdLogin);
                data.result = JsonConvert.SerializeObject(dt, Formatting.Indented);
                if ((int)dt.Rows[0][0] == 1)
                {
                    data.message = "Se módifico correctamente";
                }
                else
                {
                    data.message = "Error al Modificar";
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
        public JsonResult DeleteVendor(int IdVendor)
        {

            try
            {

                dt = service.DeleteVendor(IdVendor);
                data.result = JsonConvert.SerializeObject(dt, Formatting.Indented);
                if ((int)dt.Rows[0][0] == 1)
                {

                    data.message = "Se elimino correctamente";
                    jsonString = JsonConvert.SerializeObject(data);
                }
                else
                {
                    data.message = "Error en la operación, o no se puede eliminar porque el vendedor tiene prestamos asociados";
                    data.status = "error";
                }

                data.result = JsonConvert.SerializeObject(dt, Formatting.Indented);
            }
            catch (Exception ex)
            {
                data.message = ex.Message;
                data.status = "error";
                return Json(data,JsonRequestBehavior.AllowGet);

            }

            return Json(data, JsonRequestBehavior.AllowGet); ;


        }

        
        public JsonResult GetVendorById(int IdVendor)
        {
            try
            {
                dt = service.GetVendorById(IdVendor);
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
    }//FIN
}