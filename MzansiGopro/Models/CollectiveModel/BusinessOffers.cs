using System;
using System.Collections.Generic;
using System.Text;

namespace MzansiGopro.Models.CollectiveModel
{
   public class BusinessOffers
    {

        public string ProductListID { get; set; }

        public List<ProductListModel> OfferList { get; set; }
    }
}
