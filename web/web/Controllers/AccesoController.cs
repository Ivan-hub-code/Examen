using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using web.Models;

namespace web.Controllers
{
    public class AccesoController : Controller
    {
        // GET: Acceso
        public ActionResult Vista()
        {
            var url = $"https://localhost:44317/api/Usuarios";
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

           

            Models.Usuario atri1 = new Models.Usuario();


            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    //if (strReader == null) return;

                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        List<Models.Usuario> data = new JavaScriptSerializer().Deserialize<List<Models.Usuario>>(responseBody);
                        ViewBag.data = data;
                    }

                }
            }



            var url1 = $"https://localhost:44317/api/Georreferencias";
            var request1 = (HttpWebRequest)WebRequest.Create(url1);
            request1.Method = "GET";
            request1.ContentType = "application/json";
            request1.Accept = "application/json";



            Models.Georreferencia geo = new Models.Georreferencia();


            using (WebResponse response1 = request1.GetResponse())
            {
                using (Stream strReader1 = response1.GetResponseStream())
                {
                    //if (strReader == null) return;

                    using (StreamReader objReader1 = new StreamReader(strReader1))
                    {
                        string responseBody = objReader1.ReadToEnd();
                        List<Models.Georreferencia> data = new JavaScriptSerializer().Deserialize<List<Models.Georreferencia>>(responseBody);
                        ViewBag.datageo = data;
                    }

                }
            }



            var url2 = $"https://localhost:44317/api/Permisos";
            var request2 = (HttpWebRequest)WebRequest.Create(url2);
            request2.Method = "GET";
            request2.ContentType = "application/json";
            request2.Accept = "application/json";



            Models.Permisos permi = new Models.Permisos();


            using (WebResponse response2 = request2.GetResponse())
            {
                using (Stream strReader2 = response2.GetResponseStream())
                {
                    //if (strReader == null) return;

                    using (StreamReader objReader2 = new StreamReader(strReader2))
                    {
                        string responseBody2 = objReader2.ReadToEnd();
                        List<Models.Permisos> data = new JavaScriptSerializer().Deserialize<List<Models.Permisos>>(responseBody2);
                        ViewBag.datapermisos = data;
                    }

                }
            }
            return View();
        }
        public JsonResult Ingre(Usuario user)
        {
            try
            {
                insertuserAsync(user);
                return Json(new { status = false, message = "Exito" }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                return Json(new { status = false, message = "Aldo salio mal" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public async Task<JsonResult> insertuserAsync(Models.Usuario user)
        {
            try
            {

                var payload = new Usuario
                {

                    Nombre = user.Nombre,
                    Contraseña = user.Contraseña,
                    creacion = DateTime.Now,
                    RFC = user.RFC
                };

          
                var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(payload));

                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {

                    var httpResponse = await httpClient.PostAsync("https://localhost:44317/api/Usuarios", httpContent);

                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();

                        return Json(new { status = true, message = "Exito" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = false, message = "Aldo salio mal" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        static HttpClient client = new HttpClient();
        public async Task<JsonResult> ActuaUserAsync(Usuario atri)
        {

            var payload = new Usuario
            {
                idUsuario = atri.idUsuario,
                Nombre = atri.Nombre,
                Contraseña = atri.Contraseña,
                creacion = DateTime.Now,
                RFC = atri.RFC
            };
            var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(payload));

            var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {

                var url = $"https://localhost:44317/api/Usuarios/" + atri.idUsuario +"/"+ httpContent;
                var httpResponse = await httpClient.PutAsync(url, httpContent);

                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    return Json(new { status = true, message = "Exito" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "Aldo salio mal" }, JsonRequestBehavior.AllowGet);
                }
            }
         
        }

        public async Task<JsonResult> Delete(int id)
        {

            
            using (var httpClient = new HttpClient())
            {

                var url = $"https://localhost:44317/api/Usuarios/" + id;
                var httpResponse = await httpClient.DeleteAsync(url);

                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    return Json(new { status = true, message = "Exito" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "Aldo salio mal" }, JsonRequestBehavior.AllowGet);
                }
            }

        }



        //GeoReferencia


        [HttpPost]
        public async Task<JsonResult> insertGeoReAsync(Models.Georreferencia atri)
        {
            try
            {

                var payload = new Georreferencia
                {
                    idEstado=atri.idEstado,
                    Latitud=atri.Latitud,
                    Longitud=atri.Longitud,
                };


                var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(payload));

                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {

                    var httpResponse = await httpClient.PostAsync("https://localhost:44317/api/Georreferencias", httpContent);

                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();

                        return Json(new { status = true, message = "Exito" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = false, message = "Aldo salio mal" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public async Task<JsonResult> DeleteRef(int id)
        {


            using (var httpClient = new HttpClient())
            {

                var url = $"https://localhost:44317/api/Georreferencias/" + id;
                var httpResponse = await httpClient.DeleteAsync(url);

                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    return Json(new { status = true, message = "Exito" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "Aldo salio mal" }, JsonRequestBehavior.AllowGet);
                }
            }

        }



        //Permisos


        [HttpPost]
        public async Task<JsonResult> insertPerReAsync(Models.Permisos atri)
        {
            try
            {

                var payload = new Permisos
                {
                    idEstado=atri.idEstado,
                    idUsuario=atri.idUsuario,
                };


                var stringPayload = await Task.Run(() => JsonConvert.SerializeObject(payload));

                var httpContent = new StringContent(stringPayload, Encoding.UTF8, "application/json");

                using (var httpClient = new HttpClient())
                {

                    var httpResponse = await httpClient.PostAsync("https://localhost:44317/api/Permisos", httpContent);

                    if (httpResponse.Content != null)
                    {
                        var responseContent = await httpResponse.Content.ReadAsStringAsync();

                        return Json(new { status = true, message = "Exito" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { status = false, message = "Aldo salio mal" }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = false, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }


        public async Task<JsonResult> DeletePermisos1(int id)
        {


            using (var httpClient = new HttpClient())
            {

                var url = $"https://localhost:44317/api/Permisos/" + id;
                var httpResponse = await httpClient.DeleteAsync(url);

                if (httpResponse.Content != null)
                {
                    var responseContent = await httpResponse.Content.ReadAsStringAsync();

                    return Json(new { status = true, message = "Exito" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { status = false, message = "Aldo salio mal" }, JsonRequestBehavior.AllowGet);
                }
            }

        }
    }
}