using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication.ServiceReference;

namespace WebApplication.Models
{
    public class item2
    {
        public Usuario Product
        {
            get;
            set;
        }

        public int Quantity
        {
            get;
            set;
        }


        public byte[] img { get; set; }
    }
}