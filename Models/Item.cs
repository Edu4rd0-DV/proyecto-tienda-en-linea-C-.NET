using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.ServiceReference;

namespace WebApplication.Models
{
    public class Item 
    {  
            public Inventariob Product
            {
                get;
                set;
            }

            public int Quantity
            {
                get;
                set;
            }


            public string img { get; set; }


    }
    
      
}
