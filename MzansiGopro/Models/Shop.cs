using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MzansiGopro.Models.microModel;
using MzansiGopro.Models.CollectiveModel;


namespace MzansiGopro.Models
{
   public class Shop
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Cover_Img { get; set; }
        public string Bio { get; set; }
        public List<offer> Offers { get; set; }
        public List<offer> ExtraList { get; set; }
        public Dictionary<string, DateTime> Dates { get; set; }
        public string Location { get; set; }
        public List<image> StoreImage { get; set; }
        public Hashtable ProductList { get; set; }
        public string BusinessOfferID { get; set; }
        public BusinessOffers BusinessOffers { get; set; }
        
        public string Poster { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public DateTime ExpireDate { get; set; }
        public double Points { get; set; }
        public int Visits { get; set; }

    }
}
