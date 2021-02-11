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
    public class UserController : Controller
    {
        public string userNameLogin = "";
        public int userIdLogin;
        public int userIdProfile;
        public UserController()
        {
            userNameLogin = System.Web.HttpContext.Current.Session["userName"]?.ToString();
            userIdLogin = System.Web.HttpContext.Current.Session["idUser"] != null ? (int)System.Web.HttpContext.Current.Session["idUser"] : 0;
            userIdProfile = System.Web.HttpContext.Current.Session["idProfile"] != null ? (int)System.Web.HttpContext.Current.Session["idProfile"] : 0;
        }

        DataTable dt = null;
        public int idUser = 0;
        ResultModel data = new ResultModel();
        Service.UserService service = new Service.UserService();

        // GET: User
        public ActionResult Principal()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Login");

            return View();
        }

        public JsonResult AddUser( string name, string descriptionUser, int idProfile, bool active, string pass)
        {
            try
            {
                var _pass = "";

                if (pass != "")
                {

                    _pass = new SecurityService().Encryp(pass);

                }


                var existUser = service.GetAllUser().AsEnumerable().ToList().Where(x => x.Field<string>("name").ToUpper() == name.ToUpper()).Count();

                if ( existUser > 0)
                {
                    data.message = "Ya existe el usuario ingresado";
                    data.status = "error";
                    return Json(data, JsonRequestBehavior.AllowGet);

                }


                dt = service.AddUser( name, descriptionUser, idProfile, active, _pass, userNameLogin);
                data.result = JsonConvert.SerializeObject(dt, Formatting.Indented);
                data.message = "Se creó el usuario con éxito";

            }
            catch (Exception ex)
            {
                data.message = ex.Message;
                data.status = "error";
                return Json(data, JsonRequestBehavior.AllowGet);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllUser()
        {
            try
            {
                dt = service.GetAllUser(); 
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

        public JsonResult ModifyUser(int idUser, string name, string userDescription, int idProfile, bool active, string pass)
        {

            try
            {
                var _pass = "";

                if (pass != "") {

                    _pass = new SecurityService().Encryp(pass);

                }

                dt = service.ModifyUser(idUser, name, userDescription, idProfile, active, _pass, userNameLogin);
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
        public JsonResult DeleteUser(int idUser)
        {

            try
            {

                dt = service.DeleteUser(idUser);
                data.result = JsonConvert.SerializeObject(dt, Formatting.Indented);
                if ((int)dt.Rows[0][0] == 1)
                {

                    data.message = "Se elimino correctamente";
                }
                else
                {
                    data.message = "Error en la operación, o no se puede eliminar porque el usuario tiene préstamos asociados";
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

            return Json(data, JsonRequestBehavior.AllowGet); ;


        }


        public JsonResult GetUserById(int idUser)
        {
            try
            {
                dt = service.GeUserById(idUser);
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

        public JsonResult GetAllUserPermits(int idUser)
        {
            try
            {
                dt = service.GetAllUserPermits(idUser);
                var dt2 = dt.AsEnumerable().GroupBy(x => x.Field<string>("application"));
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
        public JsonResult ModifyPermits(int IdPermit, string Permits, bool Active, int IdUser)
        {

            try
            {
                dt =  service.ModifyPermits(IdPermit, Permits, Active, IdUser, userNameLogin);
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

    }
}