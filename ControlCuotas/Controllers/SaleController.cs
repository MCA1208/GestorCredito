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

        public JsonResult AddSale(DateTime saleDate, int idClient, int idVendor, string subTotalSale, string totalSale, int quotaSale, int? interest,
            int? discount, DateTime? dateEnd, string productString)
        {
            try
            {
                dt = Service.AddSale(saleDate, idClient, idVendor, subTotalSale, totalSale, quotaSale, interest, discount, dateEnd, productString,userNameLogin, userIdLogin);
                if ((int)dt.Rows[0][0] == 0)
                {
                    data.status = "error";
                    data.message = "error al agregar la venta";
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

    }//FIN
}