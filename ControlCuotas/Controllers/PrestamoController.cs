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
    public class PrestamoController : Controller
    {
        public string userNameLogin = "";
        public int userIdLogin;
        public int userIdProfile;
        public PrestamoController()
        {
            userNameLogin = System.Web.HttpContext.Current.Session["userName"]?.ToString();
            userIdLogin = System.Web.HttpContext.Current.Session["idUser"] != null ? (int)System.Web.HttpContext.Current.Session["idUser"] : 0;
            userIdProfile = System.Web.HttpContext.Current.Session["idProfile"] != null ? (int)System.Web.HttpContext.Current.Session["idProfile"] : 0;
        }

        DataTable dt = null;
        DataTable ExistPrestamo = null;
        public int idUser = 0;
        ResultModel data = new ResultModel();
        Service.PrestamoService Service = new Service.PrestamoService();

        Service.ClientService ServiceClient = new Service.ClientService();
        // GET: Cuotas

        public ActionResult Principal()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Login");

            return View();
        }

        public JsonResult GetAllPrestamo()
        {
            try
            {

                dt = Service.GetAllPrestamo(userIdLogin, userIdProfile);

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

        public JsonResult GetClientCombo()
        {
            try
            {

                dt = ServiceClient.GetClientCombo(userIdLogin, userIdProfile);

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

        public JsonResult AddPrestamo(int cboCliente, string concepto, string amount, string amountInterest, int quantity, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                ExistPrestamo = Service.ExistPrestamo(cboCliente, dateStart, dateEnd);

                if ((int)ExistPrestamo.Rows[0][0] == 1)
                {
                    data.message = "Ya existe un préstamo con el rango de las fechas ingresadas para este cliente";
                    data.status = "error";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

                dt = Service.AddPrestamo(cboCliente, concepto, amount, amountInterest, quantity, dateStart, dateEnd, userNameLogin, userIdLogin) ;

                if ( (int)dt.Rows[0]["result"] == 0)
                {
                    data.message = "No se pudo insertar el prestamo";
                    data.status = "error";
                }

                data.message = "Se agrego el prestamo correctamente";
            }
            catch (Exception ex)
            {
                data.message = ex.Message;
                data.status = "error";
                return Json(data, JsonRequestBehavior.AllowGet);

            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        

        public JsonResult GetPrestamoDetailById(int IdPrestamo)
        {
            try
            {

                dt = Service.GetPrestamoDetailById(IdPrestamo);

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

        public JsonResult GetPrestamoDetail(int IdPrestamo)
        {
            try
            {
                dt = Service.GetPrestamoById(IdPrestamo);
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
        //
        public JsonResult SavePrestamoForId(int IdPrestamo, DateTime dateStart, DateTime dateEnd)
        {
            try
            {
                dt = Service.SavePrestamoForId(IdPrestamo, dateStart, dateEnd, userNameLogin, userIdLogin);

                if ((int)dt.Rows[0][0] == 0)
                {
                    data.message = "Error al editar el prestamo";
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

        public JsonResult DeletePrestamo(int IdPrestamo)
        {
            try
            {
                dt = Service.DeletePrestamo(IdPrestamo);

                if ((int)dt.Rows[0][0] == 0)
                {
                    data.message = "Error al eliminar el prestamo";
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

        //En clase
    }
}