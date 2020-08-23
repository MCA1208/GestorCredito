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
    public class ClientController : Controller
    {
        public string userNameLogin = "";
        public int userIdLogin;
        public int userIdProfile;
        public ClientController()
        {
            userNameLogin = System.Web.HttpContext.Current.Session["userName"]?.ToString();
            userIdLogin = System.Web.HttpContext.Current.Session["idUser"] != null ? (int)System.Web.HttpContext.Current.Session["idUser"] : 0;
            userIdProfile = System.Web.HttpContext.Current.Session["idProfile"] != null ? (int)System.Web.HttpContext.Current.Session["idProfile"] : 0;
        }

        DataTable dt = null;
        public int idUser = 0;
        ResultModel data = new ResultModel();
        Service.PrestamoService Service = new Service.PrestamoService();


        Service.ClientService ServiceClient = new Service.ClientService();
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Principal()
        {
            if (System.Web.HttpContext.Current.Session["idUser"] == null)
                return RedirectToAction("index", "Login");

            return View();
        }


        public JsonResult CreateClient(string name  ,string dni, string  address, string  phone, int   zone, DateTime? birthDate, bool? married, string conyuge, string dniConyuge, int cboSitCred)
        {
            try
            {
                idUser = (int)System.Web.HttpContext.Current.Session["idUser"];

                DataTable getExistClient = ServiceClient.GetClientByDNI(dni);

                if (getExistClient.Rows.Count > 0)
                {
                    data.status = "error";
                    data.message = "Ya existe el Cliente Ingresado";
                    return Json(data, JsonRequestBehavior.AllowGet);

                }


                dt = ServiceClient.CreateClient(name, dni, address, phone, zone, birthDate, married, conyuge, dniConyuge, cboSitCred, userNameLogin, userIdLogin);

                if ((int)dt.Rows[0]["result"] == 0)
                {
                    data.status = "error";
                    data.message = "error al agregar el cliente";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

                data.message = "Se creó el cliente con éxito";

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

        public JsonResult GetAllClient()
        {
            try
            {
                dt = ServiceClient.GetAllClient(userIdLogin, userIdProfile);

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

  
        public JsonResult GetComboZona()
        {
            try
            {

                dt = ServiceClient.GetComboZona(userIdLogin, userIdProfile);

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

        
        public JsonResult GetClientById(int IdClient)
        {
            try
            {

                dt = ServiceClient.GetClientById(IdClient);

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

        
        public JsonResult ModifyClient(int IdClient, string name, string dni, string address, string phone, int zone, DateTime? birthDate, bool? married, string conyuge, string dniConyuge, int cboSitCred)
        {

            try
            {

                dt = ServiceClient.ModifyClient(IdClient, name, dni, address, phone, zone, birthDate, married, conyuge,dniConyuge, cboSitCred, userNameLogin, userIdLogin);

                if ((int)dt.Rows[0][0] == 1){

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
        
        public JsonResult DeleteCli(int IdClient)
        {

            try
            {

                dt = ServiceClient.DeleteClient(IdClient);

                if ((int)dt.Rows[0][0] == 1)
                {

                    data.message = "Se elimino correctamente";
                }
                else
                {
                    data.message = "Error en la operación, o no se puede eliminar porque el cliente tiene prestamos asociados";
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

    }//fin class
}