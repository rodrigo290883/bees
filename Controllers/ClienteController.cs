using System;
using System.Web;
using System.Diagnostics;
using System.Data;
using System.Data.SqlClient;
using Bees.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;

namespace Bees.Views.Cliente
{
    public class ClienteController : Controller
    {
        private readonly IConfiguration _configuration;

        public ClienteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            string usuario = HttpContext.Session.GetString("usuario");

            if (usuario != null)
            {

                Clientes clientes = new Clientes();

                List<Clientes> lst = new List<Clientes>();

                string connString = _configuration.GetConnectionString("MyConnection");
                using (SqlConnection conn = new SqlConnection(connString)) 
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT idsap,nombre,establecimiento,drv,estado,app,registro,ultimo_pedido,adopcion_mens,dia_pedido,dia_llamada FROM clientes", conn);

                    SqlDataReader sqlReader = cmd.ExecuteReader();

                    while (sqlReader.Read())
                    {
                        lst.Add(new Clientes { idsap = sqlReader.GetInt32(0), nombre = sqlReader[1].ToString(), establecimiento = sqlReader[2].ToString(), drv = sqlReader[3].ToString(), estado = sqlReader[4].ToString(), app = sqlReader.GetInt32(5), registro = sqlReader.GetInt32(6), ultimo_pedido = sqlReader[7].ToString(), adopcion_mens = sqlReader.GetDouble(8), dia_pedido = sqlReader[9].ToString(), dia_llamada = sqlReader[10].ToString() }); ;
                    }

                    ViewBag.Clientes = lst;

                    sqlReader.Close();

                    conn.Close();

                    return View();

                }
            }
            else
                return RedirectToAction("Index", "Home");
        }

        public List<Clientes> GetClientes() {
            List<Clientes> lst = new List<Clientes>();

            string connString = _configuration.GetConnectionString("MyConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand ("SELECT idsap,nombre,establecimiento,drv,estado,app,registro,ultimo_pedido,adopcion_mens,dia_pedido,dia_llamada FROM clientes", conn);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read()) {
                    lst.Add(new Clientes { idsap = sqlReader.GetInt32(0), nombre = sqlReader[1].ToString(), establecimiento = sqlReader[2].ToString(), drv = sqlReader[3].ToString(), estado = sqlReader[4].ToString(), app = sqlReader.GetInt32(5), registro = sqlReader.GetInt32(6), ultimo_pedido = sqlReader[7].ToString(), adopcion_mens = sqlReader.GetDouble(8), dia_pedido = sqlReader[9].ToString(), dia_llamada = sqlReader[10].ToString() });
                }

                conn.Close();

                return lst;
            }
        }

        public List<Clientes> GetInfoCliente(int idsap)
        {

            List<Clientes> lst = new List<Clientes>();

            string connString = _configuration.GetConnectionString("MyConnection");
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT idsap,nombre,establecimiento,drv,estado,app,registro,ultimo_pedido,adopcion_mens,dia_pedido,dia_llamada FROM clientes", conn);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    lst.Add(new Clientes { idsap = sqlReader.GetInt32(0), nombre = sqlReader[1].ToString(), establecimiento = sqlReader[2].ToString(), drv = sqlReader[3].ToString(), estado = sqlReader[4].ToString(), app = sqlReader.GetInt32(5), registro = sqlReader.GetInt32(6), ultimo_pedido = sqlReader[7].ToString(), adopcion_mens = sqlReader.GetDouble(8), dia_pedido = sqlReader[9].ToString(), dia_llamada = sqlReader[10].ToString() });
                }

                ViewBag.Clientes = lst;

                sqlReader.Close();

                conn.Close();

                return lst;
            }
        }


        /*public List<Empleados> BuscarSap(string valor)
        {

            List<Empleados> lst = new List<Empleados>();

            string connString = _configuration.GetConnectionString("MyConnection");

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT idsap,nombre,area,banda FROM empleados WHERE idsap LIKE '" + valor + "%';", conn);
                cmd.Parameters.AddWithValue("@valor", valor);

                SqlDataReader sqlReader = cmd.ExecuteReader();

                while (sqlReader.Read())
                {
                    lst.Add(new Empleados { idsap = sqlReader.GetInt32(0), nombre = sqlReader[1].ToString(), area = sqlReader[2].ToString(), banda = sqlReader[3].ToString() });
                }

                conn.Close();
                return lst;
            }
        }*/
    }
}