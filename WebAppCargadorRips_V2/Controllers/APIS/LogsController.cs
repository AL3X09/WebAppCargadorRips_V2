﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Security.Permissions;
using System.Web;
using System.Web.Http;
using WebAppCargadorRips_V2.EF_Models;

namespace WebAppCargadorRips_V2.Controllers.APIS
{
    public class LogsController : ApiController
    {
        private static DateTime fechaActual = DateTime.Now;
        private RipsEntitiesConnection bd = new RipsEntitiesConnection();
        private static string pathlogs = HttpContext.Current.Server.MapPath("~/Logs/");
        private static string nombrefile = pathlogs + "/log_" + fechaActual.Year + ".txt";
        private string Texto;


        //Constructor
        public LogsController(string texto)
        {
            Texto = texto;
            //createFolder();
        }

        public LogsController()
        {
        }

        //creo el folder
        public void createFolder()
        {
            try
            {
                //consulto que exista el folder raiz
                if (!Directory.Exists(pathlogs))
                {
                    Directory.CreateDirectory(pathlogs);
                    var permisos = new FileIOPermission(FileIOPermissionAccess.AllAccess, pathlogs);
                    var permisosSET = new PermissionSet(PermissionState.None);
                    permisosSET.AddPermission(permisos);
                    if (permisosSET.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
                    {
                    }

                }
                createFile(Texto);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        //creo el archivo y texto
        public void createFile(string texto)
        {
            try
            {

                // Creo texto en el archivo
                FileStream fs = null;
                if (!File.Exists(nombrefile))
                {
                    using (fs = File.Create(nombrefile))
                    {

                    }
                }

                writeinFile(nombrefile, texto);
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

        //agrego texto al archivo y texto
        public void writeinFile(string file, string texto)
        {
            try
            {

                if (File.Exists(file))
                {
                    File.AppendAllText(file, string.Concat("\r\n-------------------------------------- ", fechaActual, " --------------------------\r\n", texto, "\r\n"));
                    //using (StreamWriter sw = new StreamWriter(file))
                    //{
                    //sw.Write(texto);
                    //sw.WriteLine(string.Concat("-------------------------------------- ", fechaActual, " --------------------------\r\n"));
                    //sw.WriteLine(texto);
                    //}
                }
            }
            catch (Exception e)
            {
                Console.Write(e);
            }
        }

    }
}
