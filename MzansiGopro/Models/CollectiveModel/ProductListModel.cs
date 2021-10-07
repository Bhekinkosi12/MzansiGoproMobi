using System;
using System.Collections.Generic;
using System.Text;

namespace MzansiGopro.Models.CollectiveModel
{
    public class ProductListModel
    {

        public string ListID { get; set; }
        public string ListName { get; set; }
        public string Layout { get; set; }
        public List<Products> Products { get; set; }

    }

 
}
