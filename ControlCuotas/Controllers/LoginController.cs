﻿using ControlCuotas.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
               

                dt = Service.SpUserLogin(user, pass);

                if (dt.Rows.Count > 0)
                {

                    System.Web.HttpContext.Current.Session["idUser"] = dt.Rows[0]["id"];

                    data.url = Url.Action("Principal", "Prestamo");
                }
                else
                {
                    data.message = "las Credenciales ingresadas no son validas";
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
    }
}