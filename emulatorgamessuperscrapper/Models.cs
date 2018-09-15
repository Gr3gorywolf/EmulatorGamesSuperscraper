using System;
using System.Collections.Generic;
using System.Text;

namespace emulatorgamessuperscrapper
{
  public static  class Models
    {
        public  class romsinfos {
           public string nombre { get; set; }
           public string imagen { get; set; }
           public string descargas { get; set; }
           public string link { get; set; }
        };
        public class rominfo {
            public string id { get; set; }
            public string nombre { get; set; }
            public string imagen { get; set; }
            public string descargas { get; set; }
            public string linkdescarga { get; set; }
            public string consola { get; set; }
            public string region { get; set; }
            public string votos { get; set; }
            public string size { get; set; }

        }


    }
}
