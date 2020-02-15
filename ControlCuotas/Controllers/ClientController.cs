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
            return View();
        }


        public JsonResult CreateClient(string name  ,string dni, string  address, string  phone, int   zone, DateTime? birthDate, bool? married, string conyuge)
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


                dt = Service.CreateClient(name, dni, address, phone, zone, birthDate, married, conyuge);

                if ((int)dt.Rows[0][0] == 0)
                {
                    data.status = "error";
                    data.message = "error al agregar el cliente";
                    return Json(data, JsonRequestBehavior.AllowGet);
                }

                data.message = "Se creo el cliente con exito";

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
                //idUser = (int)System.Web.HttpContext.Current.Session["idUser"];


                dt = Service.GetAllClient();



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

                dt = ServiceClient.GetComboZona();

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

        
        public JsonResult ModifyClient(int IdClient, string name, string dni, string address, string phone, int zone, DateTime? birthDate, bool? married, string conyuge)
        {

            try
            {

                dt = ServiceClient.ModifyClient(IdClient, name, dni, address, phone, zone, birthDate, married, conyuge);

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

    }
}