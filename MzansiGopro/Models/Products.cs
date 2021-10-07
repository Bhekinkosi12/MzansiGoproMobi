using System;
using System.Collections.Generic;
using System.Text;
using MzansiGopro.Models.microModel;

namespace MzansiGopro.Models
{
   public class Products
    {

        public string Id { get; set; }
        public bool IsSale { get; set; }
        public double SalePrice { get; set; }
        public string SaleNote { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public List<offer> Ingredient { get; set; }
        public List<offer> OptionalExtra { get; set; }

    }
}
