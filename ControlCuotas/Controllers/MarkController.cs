using ControlCuotas.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace ControlCuotas.Controllers
{
    public class MarkController : Controller
    {
        public string userNameLogin = "";
        public int userIdLogin;
        public int userIdProfile;
        public MarkController()
        {
            userNameLogin = System.Web.HttpContext.Current.Session["userName"]?.ToString();
            userIdLogin = System.Web.HttpContext.Current.Session["idUser"] != null ? (int)System.Web.HttpContext.Current.Session["idUser"] : 0;
            userIdProfile = System.Web.HttpContext.Current.Session["idProfile"] != null ? (int)System.Web.HttpContext.Current.Session["idProfile"] : 0;
        }

        DataTable dt = null;
        ResultModel data = new ResultModel();
        Service.MarkService Service = new Service.MarkService();
        // GET: Mark
        public ActionResult Principal()
        {
            if (userIdLogin == 0)
                return RedirectToAction("index", "Login");

            return View();
        }

        public JsonResult GetAllMark()
        {
            try
            {
                dt = Service.GetAllMark(userIdLogin, userIdProfile);
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

        public JsonResult AddMark(string name)
        {
            try
            {
                dt = Service.AddMark(name, userNameLogin,userIdLogin);
                if ((int)dt.Rows[0][0] == 0)
                {
                    data.status = "error";
                    data.message = "error al agregar la marca";
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
        public JsonResult GetMarkById(int IdMark)
        {
            try
            {
                dt = Service.GetMarkById(IdMark);

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

        public JsonResult ModifyMark(int idMark, string name)
        {
            try
            {
                dt = Service.ModifyMark(idMark, name, userNameLogin, userIdLogin);

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


        public JsonResult DeleteMark(int idMark)
        {
            try
            {
                dt = Service.DeleteMark(idMark);

                data.result = dt.Rows[0][0];

                if ((int)dt.Rows[0][0] == 1)
                {
                    data.message = "Se eliminó correctamente";
                }
                else
                {
                    data.message = "Error en la operación, o no se puede eliminar porque la marca tiene productos asociados";
                    data.status = "error";
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

    }//FIN
}