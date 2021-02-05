using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace web.Models
{
    public class Clases
    {



        public Usuario GetItems(string filter)
        {
            var url = $"https://localhost:44317/api/Usuarios/" + filter;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            Usuario atri = new Usuario();
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


                            atri = new JavaScriptSerializer().Deserialize<Usuario>(json);

        
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


    public class Usuario
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