using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList.Mvc;
using PagedList;

namespace WebApplication.Models
{
    public class inventario_p
    {
        
        public Int64 id { get; set; }
  
        public Int64 id_producto { get; set; }

        public Int64 Cantidad { get; set; }
     
        public decimal Precio { get; set; }
   
        public Int64 id_proveedor { get; set; }
     
        public int sexo { get; set; }
       
        public string fecha_e { get; set; }
        
        public string imagen { get; set; }

        public string descripcion { get; set; }

        public string DataProducto { get; set; }

       
        public string DataProveedor { get; set; }

        public string Datasexo { get; set;  }
    }
}