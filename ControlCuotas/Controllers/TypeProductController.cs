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
    public class TypeProductController : Controller
    {
        public string userNameLogin = "";
        public int userIdLogin;
        public int userIdProfile;
        public TypeProductController()
        {
            userNameLogin = System.Web.HttpContext.Current.Session["userName"]?.ToString();
            userIdLogin = System.Web.HttpContext.Current.Session["idUser"] != null ? (int)System.Web.HttpContext.Current.Session["idUser"] : 0;
            userIdProfile = System.Web.HttpContext.Current.Session["idProfile"] != null ? (int)System.Web.HttpContext.Current.Session["idProfile"] : 0;
        }
        DataTable dt = null;
        ResultModel data = new ResultModel();
        Service.TypeProductService Service = new Service.TypeProductService();
        // GET: TypeProduct
        public ActionResult Principal()
        {
            if (userIdLogin == 0)
                return RedirectToAction("index", "Login");

            return View();
        }
        public JsonResult GetAllTypeProduct()
        {
            try
            {
                dt = Service.GetAllTypeProduct(userIdLogin, userIdProfile);
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

        public JsonResult AddTypeProduct(string name)
        {
            try
            {
                dt = Service.AddTypeProduct(name, userNameLogin, userIdLogin);
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
        public JsonResult GetTypeProductById(int IdTypeProduct)
        {
            try
            {
                dt = Service.GetTypeProductById(IdTypeProduct);

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

        public JsonResult ModifyTypeProduct(int IdTypeProduct, string name)
        {
            try
            {
                dt = Service.ModifyTypeProduct(IdTypeProduct, name, userNameLogin, userIdLogin);

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


        public JsonResult DeleteTypeProduct(int idTypeProduct)
        {
            try
            {
                dt = Service.DeleteTypeProduct(idTypeProduct);

                data.result = dt.Rows[0][0];

                if ((int)dt.Rows[0][0] == 1)
                {
                    data.message = "Se eliminó correctamente";
                }
                else
                {
                    data.message = "Error en la operación, o no se puede eliminar porque el el tipo de producto tiene productos asociados";
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