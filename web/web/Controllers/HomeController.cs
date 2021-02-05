using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

using System.Web.Script.Serialization;
namespace web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult Logeo(string user, string pass)
        {
            try
            {
                var usur= GetItems(user);
                if(pass == usur.Contraseña)
                {
                    
                    var per = Permisos(usur.idUsuario);
                    var geo = GeoGet(per.idEstado);
                    if (user != null)
                    {
                        return Json(1);
                    }
                    else
                    {
                        return Json(0);
                    }
                }
                else
                {
                    return Json(new { status = false, message = "Contraseña incorrecta" }, JsonRequestBehavior.AllowGet);
                }
                
                
            }
            catch(Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public Georreferencia GeoGet(int? filter)
        {
            var url = $"https://localhost:44317/api/Georreferencias/" + filter;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            Georreferencia atri = new Georreferencia();
            string json = "";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        //if (strReader == null) return;

                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            // Do something with responseBody
                            json = responseBody;


                            atri = new JavaScriptSerializer().Deserialize<Georreferencia>(json);

                            Session["referencia"] = atri;
                        }

                    }
                }
                return atri;
            }
            catch (WebException ex)
            {
                return atri;
            }
        }


        public Atri  GetItems(string filter)
        {
            var url = $"https://localhost:44317/api/Usuarios/"+filter;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            Atri atri = new Atri();
            string json="";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        //if (strReader == null) return;

                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            // Do something with responseBody
                            json = responseBody;


                            atri = new JavaScriptSerializer().Deserialize<Atri>(json);

                            Session["user"] = atri;
                        }

                    }
                }
                return atri;
            }
            catch (WebException ex)
            {
                return atri;
            }
        }


        public Permisos Permisos(int filter)
        {
            var url = $"https://localhost:44317/api/Permisos/" + filter;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            Permisos atri = new Permisos();
            string json = "";
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        //if (strReader == null) return;

                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();

                            // Do something with responseBody
                            json = responseBody;


                            atri = new JavaScriptSerializer().Deserialize<Permisos>(json);

                            Session["referencia"] = atri;
                        }

                    }
                }
                return atri;
            }

            catch (WebException ex)
            {
                return atri;
            }
        }


    }

    


public class Atri
    {
        public int idUsuario { get; set; }
        public string Contraseña { get; set; }
        public string Nombre { get; set; }
        public Nullable<System.DateTime> creacion { get; set; }
        public string RFC { get; set; }
    }


    public class Georreferencia
    {
        public int idGeorreferencia { get; set; }
        public Nullable<int> idEstado { get; set; }
        public Nullable<double> Latitud { get; set; }
        public Nullable<double> Longitud { get; set; }
    }

    public class Permisos
    {
        public int idUsuario { get; set; }
        public Nullable<int> idEstado { get; set; }
    }
}