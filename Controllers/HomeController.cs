using System;
using System.Web;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Bees.Models;
using Microsoft.AspNetCore.Http;

namespace Bees.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string usuario = HttpContext.Session.GetString("usuario");
            if (usuario == null)
                return View();
            else
                return RedirectToAction("Index", "Cliente");
        }


        public ActionResult Entrar(string usuario, string contrasena)
        {
            try {
                string connString = _configuration.GetConnectionString("MyConnection");
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    if (usuario == null || contrasena == null)
                    {
                        return Content("Ingresa Usuario y Contraseña");
                    }
                    else {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT idsap From usuarios Where idsap = @idsap and contrasena = @contrasena", conn);
                        cmd.Parameters.AddWithValue("@idsap", usuario);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);

                        SqlDataReader sqlReader = cmd.ExecuteReader();

                        try
                        {
                            if (sqlReader.Read())
                            {
                                HttpContext.Session.SetString("usuario", usuario);


                                conn.Close();
                                return Content("1");
                            }
                            else {
                                
                                
                                return Content("No se encontro el usuario o contraseña");
                            }
                            
                        }
                        catch(Exception ex) {
                            
                            string note = ex.ToString();
                            return Content("No se encontro el usuario o contraseña");
                        }
                    }
                }
            }catch(Exception ex){
                return Content(ex.Message);
            }
        }

        public ActionResult Salir()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
