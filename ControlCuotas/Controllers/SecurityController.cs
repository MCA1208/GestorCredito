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
    public class SecurityController : Controller
    {
        public string userNameLogin = "";
        public int userIdLogin;
        public int userIdProfile;
        public SecurityController()
        {
            userNameLogin = System.Web.HttpContext.Current.Session["userName"]?.ToString();
            userIdLogin = System.Web.HttpContext.Current.Session["idUser"] != null ? (int)System.Web.HttpContext.Current.Session["idUser"] : 0;
            userIdProfile = System.Web.HttpContext.Current.Session["idProfile"] != null ? (int)System.Web.HttpContext.Current.Session["idProfile"] : 0;
        }

        DataTable dt = null;
        DataTable ExistPrestamo = null;
        public int idUser = 0;
        ResultModel data = new ResultModel();


        // GET: Security
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetAllPermitsByUserProgram(string nameView)
        {
            try
            {
                dt = new SecurityService().GetAllPermitsByUserProgram(userIdLogin);
                var dt2 = dt.AsEnumerable().Where(x => x.Field<string>("application").Contains(nameView)).GroupBy(x => x.Field<string>("application"));

                data.result = JsonConvert.SerializeObject(dt2, Formatting.Indented);
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