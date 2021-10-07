using System;
using System.Collections.Generic;
using System.Text;

namespace MzansiGopro.Models
{
    public class News
    {

        public string ID { get; set; }

        public string Cover { get; set; }
        public DateTime DatePosted { get; set; }
        public string Author { get; set; }
        public string Heading { get; set; }
        public string Message { get; set; }

    }
}
