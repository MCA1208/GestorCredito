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
    public class ProductController : Controller
    {
        public string userNameLogin = "";
        public int userIdLogin;
        public int userIdProfile;
        public ProductController()
        {
            userNameLogin = System.Web.HttpContext.Current.Session["userName"]?.ToString();
            userIdLogin = System.Web.HttpContext.Current.Session["idUser"] != null ? (int)System.Web.HttpContext.Current.Session["idUser"] : 0;
            userIdProfile = System.Web.HttpContext.Current.Session["idProfile"] != null ? (int)System.Web.HttpContext.Current.Session["idProfile"] : 0;
        }

        DataTable dt = null;
        ResultModel data = new ResultModel();
        Service.ProductService Service = new Service.ProductService();
        // GET: Product
        public ActionResult Principal()
        {
            if (userIdLogin == 0)
                return RedirectToAction("index", "Login");

            return View();
        }
        public JsonResult GetAllProduct()
        {
            try
            {
                dt = Service.GetAllProduct(userIdLogin, userIdProfile);
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

        public JsonResult AddProduct(string name, int idMark,int idTypeProduct, string costPrice, int stock, string salePrice)
        {
            try
            {
                dt = Service.AddProduct(name, idTypeProduct, idMark, costPrice, salePrice, stock, userNameLogin, userIdLogin);
                if ((int)dt.Rows[0][0] == 0)
                {
                    data.status = "error";
                    data.message = "error al agregar el producto";
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
        public JsonResult GetProductById(int IdProduct)
        {
            try
            {
                dt = Service.GetProductById(IdProduct);

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

        public JsonResult ModifyProduct(int idProduct, string name, int idTypeProduct, int idMark, string costPrice, string salePrice, int stock)
        {
            try
            {
                dt = Service.ModifyProduct(idProduct, name,  idTypeProduct,  idMark,  costPrice,  salePrice, stock, userNameLogin, userIdLogin);

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


        public JsonResult DeleteTypeProduct(int idProduct)
        {
            try
            {
                dt = Service.DeleteProduct(idProduct);

                data.result = dt.Rows[0][0];

                if ((int)dt.Rows[0][0] == 1)
                {
                    data.message = "Se eliminó correctamente";
                }
                else
                {
                    data.message = "Error en la operación, o no se puede eliminar porque el producto tiene ventas asociados";
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