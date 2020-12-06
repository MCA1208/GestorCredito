using ControlCuotas.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlCuotas.Controllers
{
    public class SaleController : Controller
    {
        public string userNameLogin = "";
        public int userIdLogin;
        public int userIdProfile;
        public SaleController()
        {
            userNameLogin = System.Web.HttpContext.Current.Session["userName"]?.ToString();
            userIdLogin = System.Web.HttpContext.Current.Session["idUser"] != null ? (int)System.Web.HttpContext.Current.Session["idUser"] : 0;
            userIdProfile = System.Web.HttpContext.Current.Session["idProfile"] != null ? (int)System.Web.HttpContext.Current.Session["idProfile"] : 0;
        }

        DataTable dt = null;
        ResultModel data = new ResultModel();
        Service.SaleService Service = new Service.SaleService();
        // GET: Sale
        public ActionResult Principal()
        {
            if (userIdLogin == 0)
                return RedirectToAction("index", "Login");

            return View();
        }
        public ActionResult ProductSale()
        {
            if (userIdLogin == 0)
                return RedirectToAction("index", "Login");

            return View();
        }

        public JsonResult AddSale(DateTime saleDate, int idClient, int idVendor, string subTotalSale, string totalSale, int quotaSale, int? interest,
            int? discount, DateTime? dateEnd, string productString, string quotaPrice)
        {
            try
            {
                dt = Service.AddSale(saleDate, idClient, idVendor, subTotalSale, totalSale, quotaSale, interest, discount, dateEnd, productString, quotaPrice,userNameLogin, userIdLogin);
                if (Convert.ToInt32(dt.Rows[0][0]) == 0)
                {
                    data.status = "error";
                    data.message = "error al agregar la venta";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

                data.message = "Se agrego correctamente,  el N° de venta es: " + dt.Rows[0][0];
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

        public JsonResult GetAllProductSale()
        {
            try
            {
                dt = Service.GetAllProductSale(userIdLogin, userIdProfile);
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

        public JsonResult GetSaleDetail(int IdSale)
        {
            try
            {
                dt = Service.GetSaleById(IdSale);
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
        public JsonResult SaveSaleById(int IdSale, DateTime dateStart, DateTime dateEnd, int idVendor)
        {
            try
            {
                dt = Service.SaveSaleById(IdSale, dateStart, dateEnd, userNameLogin, userIdLogin, idVendor);

                if ((int)dt.Rows[0][0] == 0)
                {
                    data.message = "Error al editar la venta";
                    data.status = "error";
                }
                else
                {
                    data.message = "Se modifico correctamente";
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
        public JsonResult GetSaleDetailById(int IdSale)
        {
            try
            {

                dt = Service.GetSaleDetailById(IdSale);

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
        public JsonResult GetCuotaDetail(int IdCuota)
        {
            try
            {

                dt = Service.GetCuotaDetail(IdCuota);

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


        public JsonResult SaveCuotaForId(int IdCuota, DateTime? fecha, string observation, string observationPartial)
        {
            try
            {

                dt = Service.SaveCuotaForId(IdCuota, fecha, observation, observationPartial, userNameLogin, userIdLogin);

                if ((int)dt.Rows[0][0] == 0)
                {
                    data.message = "Error al editar la cuota";
                    data.status = "error";
                }
                else
                {
                    data.message = "Se modifico correctamente";
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

        public JsonResult DeleteSale(int IdSale)
        {
            try
            {
                dt = Service.DeleteSale(IdSale);

                if ((int)dt.Rows[0][0] == 0)
                {
                    data.message = "Error al eliminar la venta";
                    data.status = "error";
                }
                else
                {
                    data.message = "Se elimino correctamente";
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
        public JsonResult GetSalProducteDetailById(int IdSale)
        {
            try
            {
                dt = Service.GetSalProducteDetailById(IdSale);
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