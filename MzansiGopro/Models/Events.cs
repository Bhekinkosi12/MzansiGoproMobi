using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MzansiGopro.Models
{
   public class Events
    {
        public string ID { get; set; }
        public string Idetifier { get; set; }
        public string Name  { get; set; } 
        public DateTime EventDateTime { get; set; }
        public string Location { get; set; }
        public string Cover { get; set; }
        public int UsersComing { get; set; }
        public Hashtable Updates { get; set; }

    }
}
