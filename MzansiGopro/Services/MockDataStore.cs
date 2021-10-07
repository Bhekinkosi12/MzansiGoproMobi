using MzansiGopro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MzansiGopro.Models.microModel;

namespace MzansiGopro.Services
{
    public class MockDataStore 
    {
       
        List<Shop> shoplist { get; set; }



        public MockDataStore()
        {
            setTem();
        }







        public List<Shop> ReturnList()
        {
            return shoplist;
        }





        void setTem()
        {

            shoplist = new List<Shop>()
            {
                new Shop
                {
                    Name = "Mgani Shop",
                     ID = Guid.NewGuid().ToString(),
                    Offers = new List<offer>()
                    {
                       new offer
                       {
                           Name = "Kota",

                       },
                         new offer
                       {
                           Name = "Shisa Nyama",

                       }

                    },


                    ExtraList = new List<offer>()
                    {
                        new offer
                        {
                            Name = "Free Wifi"
                        }
                    },
                     Cover_Img = "shisa1.jpg",
                     Location = "-26.5476126;29.0822674"

                },  new Shop
                {
                    ID = Guid.NewGuid().ToString(),
                    Name = "Close Shops SA",
                    Offers = new List<offer>()
                    {
                       new offer
                       {
                           Name = "Kota",

                       },
                         new offer
                       {
                           Name = "Shisa Nyama",

                       }

                    },


                    ExtraList = new List<offer>()
                    {
                        new offer
                        {
                            Name = "Free Wifi"
                        }
                    },
                     Cover_Img = "shisa2.jpg",
                     Location = "-26.5475326;29.0822629"
                },
                  new Shop
                {
                       ID = Guid.NewGuid().ToString(),
                    Name = "Mshisa Shisa nyama",
                    Offers = new List<offer>()
                    {
                       new offer
                       {
                           Name = "Kota",

                       },
                         new offer
                       {
                           Name = "Shisa Nyama",

                       }

                    },


                    ExtraList = new List<offer>()
                    {
                        new offer
                        {
                            Name = "Free Wifi"
                        }
                    },
                     Cover_Img = "shisa3.jpg",
                     Location = "-26.5776126;29.0322674"

                },
                  new Shop
                {
                       ID = Guid.NewGuid().ToString(),
                    Name = "The breeze",
                  Offers = new List<offer>()
                    {
                       new offer
                       {
                           Name = "Kota",

                       },
                         new offer
                       {
                           Name = "Shisa Nyama",

                       }

                    },


                    ExtraList = new List<offer>()
                    {
                        new offer
                        {
                            Name = "Free Wifi"
                        }
                    },
                     Cover_Img = "shisa4.jpg",
                     Location = "-26.1476126;29.0822674"

                },
                  new Shop
                {
                      ID = Guid.NewGuid().ToString(),
                    Name = "Kasi Nyama",
                     Offers = new List<offer>()
                    {
                       new offer
                       {
                           Name = "Kota",

                       },
                         new offer
                       {
                           Name = "Shisa Nyama",

                       }

                    },


                    ExtraList = new List<offer>()
                    {
                        new offer
                        {
                            Name = "Free Wifi"
                        }
                    },
                     Cover_Img = "shisa1.jpg",
                     Location = "-26.5471226;29.8022674"

                },
            };




        }






    }
}