﻿using System;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.Security.Cryptography.X509Certificates;
using System.Net.Http;
using System.Collections.Generic;
using System.Net.Security;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Collections.Specialized;
using TextCopy;
namespace emulatorgamessuperscrapper
{
    class Program
    {


     
        static superscraper escrapeador = new superscraper();
     
        static int numeroconsola = 0;
        static int paginas = 0;
        static bool hd = false;
        static bool scrapper = false;
        static string[] consolenames = { "gameboy-advance", "super-nintendo", "nintendo-64", "nintendo", "playstation", "gameboy-color", "sega-genesis", "gameboy", "dreamcast" };
     static  Dictionary<int, int> maximos = new Dictionary<int, int>();
        static Dictionary<string, List<Models.emuladores>> diccioemuladores = new Dictionary<string, List<Models.emuladores>>();
        static  void Main(string[] args)
        {
            // se le aplica la validacion falsa de certificados a la seguridad utilizada en la libreria system.net
            ServicePointManager.ServerCertificateValidationCallback += new System.Net.Security.RemoteCertificateValidationCallback(ValidateRemoteCertificate);
            // se selecciona la version de el protocolo de seguridad a la versiond e tls 1.2
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
            
             

            Console.WriteLine("Por favor seleccione lo que dese hacer");
            Console.WriteLine("1-Extraer info de rom");
            Console.WriteLine("2-Obtener info de roms");
            Console.WriteLine("3-gestionar emuladores");
            var seleccion = 0;
            try
            {

               seleccion = int.Parse(Console.ReadLine().Trim());
                if (seleccion > 3 || seleccion < 1)
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.Clear();

                Console.WriteLine("seleccion invalida por favor entre una valida");
                Console.ReadKey();
                Main(consolenames);
            }
            if (seleccion == 2)
                scrappearmenu();
            else
             if (seleccion == 1)
                getinfomenu();
            else
             if (seleccion == 3)
                gestionemuls();

            //////////////////////ignorar!! 
            /*  var escritor = File.CreateText("gba.gr3dump");
            //  string completitaxd = "";
              List<string> nombreses = new List<string>();
              List<string> linkeses = new List<string>();
              List<string> portadases = new List<string>();
              List<string> descargases = new List<string>();
              foreach (var item in resultados) {
                  nombreses.Add(item.nombre);
                  linkeses.Add(item.link);
                  portadases.Add(item.imagen);
                  descargases.Add(item.descargas);
              }
              escritor.Write(string.Join("****", nombreses) + "+++++" + string.Join("****", linkeses) + "+++++" + string.Join("****", portadases) + "+++++" + string.Join("****", descargases));
              escritor.Close();
              Console.WriteLine("Hello World!");*/
        }

        public static void getinfomenu() {
            GC.Collect(0);

            Console.Clear();
            Console.WriteLine("Por favor copie el link y presione enter");
            Console.ReadKey();
            var link = Clipboard.GetText();
            
            if (!link.Contains("https://emulator.games/roms/"))
            {
                Console.Clear();
                Console.WriteLine("link invalido");
                Console.ReadKey();
                getinfomenu();
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Extrayendo informacion...");
                /////el getrominfo.result devuelve un objeto de la clase models.rominfo 
                var info=escrapeador.getrominfo(link).Result;
                //// ese mismo objeto es convertido a string con el serializer de newtonsoft.json
                string json = JsonConvert.SerializeObject(info);

                   ///escribe el archivo .json justo en la raiz de donde se ejecuta el programa
                System.IO.File.WriteAllText(info.nombre+ ".json", json);
                Console.Clear();
                Console.WriteLine("informacion extraida correctamente en el archivo " + info.nombre + ".json" + " que esta en la carpeta raiz de el launcher");

            }

        }
        public  static void gestionemuls() {
            if (File.Exists("emulators.json"))
            {
              
                    diccioemuladores = JsonConvert.DeserializeObject<Dictionary<string, List<Models.emuladores>>>(File.ReadAllText("emulators.json"));
              
            }
            Console.Clear();
            Console.WriteLine("Seleccione la consola para agregarle un emulador");
            Console.WriteLine("0-gameboy-advance");
            Console.WriteLine("1-super-nintendo");
            Console.WriteLine("2-nintendo-64");
            Console.WriteLine("3-nintendo");
            Console.WriteLine("4-playstation");
            Console.WriteLine("5-gameboy color");
            Console.WriteLine("6-Sega genesis");
            Console.WriteLine("7-Gameboy");
            Console.WriteLine("8-Sega Dreamcast");
            try
            {

                numeroconsola = int.Parse(Console.ReadLine().Trim());
                if (numeroconsola > 8 || numeroconsola < 0)
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.Clear();

                Console.WriteLine("seleccion invalida por favor entre una valida");
                Console.ReadKey();
                gestionemuls();
            }
            bool notienekey = false;
            List<Models.emuladores> listaseleccion = new List<Models.emuladores>();
            if (diccioemuladores.ContainsKey(consolenames[numeroconsola]))
                listaseleccion = diccioemuladores[consolenames[numeroconsola]];
            else
                notienekey = true;

            Console.WriteLine("----------Emuladores para esta consola------------");
            foreach (var aax in listaseleccion)
            {
                Console.WriteLine(aax.nombre);
            }
            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("Desea agregar un nuevo emulador?");
            Console.WriteLine("1-Si");
            Console.WriteLine("2-No");
            var seleccion = 0;
            try
            {

               seleccion = int.Parse(Console.ReadLine().Trim());
                if (seleccion >2 || seleccion < 1)
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.Clear();

                Console.WriteLine("seleccion invalida por favor entre una valida");
                Console.ReadKey();
                gestionemuls();
            }
            if (seleccion == 1)

            {


                var elem = new Models.emuladores();
                Console.Clear();
                Console.WriteLine("Copie el nombre de el emulador y pulse enter");
                Console.ReadKey();
               elem.nombre= Clipboard.GetText();
                Console.WriteLine("Copie el link de el emulador y pulse enter");
                Console.ReadKey();
                elem.link= Clipboard.GetText();
                Console.WriteLine("Copie el link de la imagen de el emulador y pulse enter");
                Console.ReadKey();
                elem.imagen = Clipboard.GetText();
                Console.WriteLine("es actualmente compactible con neonrom3r?");
                Console.WriteLine("1-si");
                Console.WriteLine("2-no");
                var seleccion2 = 0;
                try
                {

                    seleccion2 = int.Parse(Console.ReadLine().Trim());
                    if (seleccion2 > 2 || seleccion2 < 1)
                        throw new Exception();
                }
                catch (Exception)
                {
                    Console.Clear();

                    Console.WriteLine("seleccion invalida por favor entre una valida");
                    Console.ReadKey();
                    gestionemuls();
                }
                if (seleccion2 == 1)
                    elem.compactible = "Compatible";
                else
                    elem.compactible = "No compatible";

                listaseleccion.Add(elem);
                if (notienekey)
                    diccioemuladores.Add(consolenames[numeroconsola], listaseleccion);
                else
                    diccioemuladores[consolenames[numeroconsola]] = listaseleccion;

                var arch = File.CreateText("emulators.json");
                arch.Write(JsonConvert.SerializeObject (diccioemuladores));
                arch.Close();
                gestionemuls();




            }
           





        }

        public static void scrappearmenu() {
            /////////////////////lista de nombres de consolas
            ////////////0-gameboy-advance
            ////////////1-super-nintendo
            ////////////2-nintendo-64
            ////////////3-nintendo
            ////////////4-playstation
            ////////////5-gameboy-color
            ////////////6-sega-genesis
            ////////////7-gameboy
            GC.Collect(0);

            Console.Clear();

            maximos.Add(0, 15);
            maximos.Add(1, 12);
            maximos.Add(2, 3);
            maximos.Add(3, 18);
            maximos.Add(4, 2);
            maximos.Add(5, 6);
            maximos.Add(6, 11);
            maximos.Add(7, 6);
            maximos.Add(8, 1);
            if (scrapper) { }
            Console.WriteLine("Seleccione la consola para extraer informacion de los roms");
            Console.WriteLine("0-gameboy-advance");
            Console.WriteLine("1-super-nintendo");
            Console.WriteLine("2-nintendo-64");
            Console.WriteLine("3-nintendo");
            Console.WriteLine("4-playstation");
            Console.WriteLine("5-gameboy color");
            Console.WriteLine("6-Sega genesis");
            Console.WriteLine("7-Gameboy");
            Console.WriteLine("8-Sega Dreamcast");
            try
            {

                numeroconsola = int.Parse(Console.ReadLine().Trim());
                if (numeroconsola > 8 || numeroconsola < 0)
                    throw new Exception();
            }
            catch (Exception)
            {
                Console.Clear();

                Console.WriteLine("seleccion invalida por favor entre una valida");
                Console.ReadKey();
                scrappearmenu();
            }
            Console.Clear();
            Console.WriteLine("Desea la extracion de portadas en hd?");
            Console.WriteLine("1-Si");
            Console.WriteLine("2-No");
            try
            {

                var seleccion = int.Parse(Console.ReadLine().Trim());
                if (seleccion > 2 || seleccion < 1)
                    throw new Exception();
                else
                if (seleccion == 1)
                    hd = true;
                else
                if (seleccion == 2)
                    hd = false;


            }
            catch (Exception)
            {
                Console.Clear();

                Console.WriteLine("seleccion invalida por favor entre una valida");
                Console.ReadKey();
                scrappearmenu();
            }

            Console.Clear();
            Console.WriteLine("Cuantas paginas desea extraer(mientras mas paginas mas tiempo tardara pero generara mas resultados)?" + "(maximo para esta consola:" + maximos.GetValueOrDefault(numeroconsola) + ")");
            try
            {

                var seleccion = int.Parse(Console.ReadLine().Trim());
                if (seleccion > maximos.GetValueOrDefault(numeroconsola) || seleccion < 0)
                    throw new Exception();
                else
                    paginas = seleccion;


            }
            catch (Exception)
            {
                Console.Clear();

                Console.WriteLine("seleccion invalida por favor entre una valida");
                Console.ReadKey();
                scrappearmenu();
            }
            Console.Clear();
            Console.WriteLine("Extrayendo informacion...");
            /////el getwebdata.result devuelve un objeto de la clase models.rominfo 
            var resultados = escrapeador.getwebdata(consolenames[numeroconsola], paginas, hd).Result;
            //     var vista = escrapeador.getrominfo(resultados[56].link).Result;
            //   var link= escrapeador.getdownloadlink(vista.id).Result;
            //// ese mismo objeto es convertido a string con el serializer de newtonsoft.json
            string json = JsonConvert.SerializeObject(resultados);

              ///escribe el archivo .json justo en la raiz de donde se ejecuta el programa
             System.IO.File.WriteAllText(consolenames[numeroconsola] + ".json", json);

            /*   var escritor = File.CreateText("dumps/"+consolenames[numeroconsola]+".gr3dump");
               //  string completitaxd = "";
               List<string> nombreses = new List<string>();
               List<string> linkeses = new List<string>();
               List<string> portadases = new List<string>();
               List<string> descargases = new List<string>();

               escritor.Write(string.Join("****", nombreses) + "+++++" + string.Join("****", linkeses) + "+++++" + string.Join("****", portadases) + "+++++" + string.Join("****", descargases));
               escritor.Close();
               Console.Clear();
             */

            int counterfound = 0;
         
            foreach (var item in resultados)
            {
                counterfound++;
                
                    
              
            }
            Console.WriteLine("informacion extraida correctamente en el archivo " + consolenames[numeroconsola] + ".json" + " que esta en la carpeta raiz de el launcher");
            Console.WriteLine("Resumen: Roms encontrados:" + counterfound);


            Console.ReadKey();
            Console.WriteLine("Que desea hacer ahora");
            Console.WriteLine("1-extraer otra vez");
            Console.WriteLine("2-salir");
            try {
                int seleccion = int.Parse(Console.ReadLine());
                if (seleccion == 1) {
                    string[] arroz = { "ddd", "fggdfdf", "" };
                     Main(arroz);
                }
            }
            catch (Exception) {

            }



        }
        //   hace que siempre el certificado sea valido para asi poder usar https
        private static bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
        {
            return true;
        }

    }
}
