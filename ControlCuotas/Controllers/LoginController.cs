using ControlCuotas.Models;
using ControlCuotas.Service;
using ControlSheet.Helper;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Web.Mvc;
using System.Web.Security;

namespace ControlCuotas.Controllers
{
    public class LoginController : Controller
    {

        DataTable dt = null;
        public int idUser = 0;
        ResultModel data = new ResultModel();
        Service.LoginService Service = new Service.LoginService();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult LoginUser(string user, string pass)
        {
            try
            {
 
                dt = Service.SpUserLogin(user);

                if (dt.Rows.Count > 0)
                {
                    int isAuth = System.Web.HttpContext.Current.Session["idUser"] != null ? (int)System.Web.HttpContext.Current.Session["idUser"] : 0;

                    if (isAuth == (int)dt.Rows[0]["id"])
                    {
                        data.message = "El usuario "+ user +" ya esta logueado";
                        data.status = "error";
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    var active = Convert.ToInt32(dt.Rows[0]["active"]);
                    if (active == 0)
                    {

                        data.message = "El usuario se encuentra inactivo";
                        data.status = "error";
                        return Json(data, JsonRequestBehavior.AllowGet);

                    }

                    var PassDB = dt.Rows[0]["password"].ToString();
                    var passDescryp = new SecurityService().Decrypt(PassDB);
                    if (passDescryp != pass)
                    {
                        data.message = "Contraseña Invalida";
                        data.status = "error";
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }


                    System.Web.HttpContext.Current.Session["idUser"] = dt.Rows[0]["id"];
                    System.Web.HttpContext.Current.Session["userName"] = dt.Rows[0]["name"];
                    System.Web.HttpContext.Current.Session["idProfile"] = dt.Rows[0]["idProfile"];

                    data.url = Url.Action("Principal", "Prestamo");
                }
                else
                {
                    data.message = "Las credenciales ingresadas no son validas";
                    data.status = "error";
                    return Json(data, JsonRequestBehavior.AllowGet);
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

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon(); // it will clear the session at the end of request
            SecurityHelper.LogOffUser();
            return RedirectToAction("index", "Login");
        }

        //Fin class
    }
}