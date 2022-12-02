using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.ServiceReference;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class cartController : Controller
    {
        Service1Client _cliente = new Service1Client();
        // GET: cart
        public ActionResult Index()
        {
            return View();
        }
        /*<---------------------------------------------------------->
        */
  
        public ActionResult Buy(Int64 id , Int64 id2)
        {
            if (id2 == 0)
            {
                return  RedirectToAction("Login", "usuario");
            }

            Inventariob _in = new Inventariob();
            List<Item> cart = new List<Item>();
            string imgDataURL = "";
            var consulta = _cliente.mostrar_inventartio_id(id);
            foreach (var item in consulta)
            {
                string imreBase64Data = Convert.ToBase64String(item.imagen);
                 imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                //ViewBag.ImageData = imgDataURL;
                cart.Add(new Item { Product = item, Quantity = 1, img = imgDataURL });
            }

            //ProductModel productModel = new ProductModel();
            if (Session["cart"] == null)
            {
                //cart.Add(new Item { Product = _cliente.Listar_Productos_carrito(id) , Quantity = 1 });
                Session["cart"] = cart;
            }
            else
            {
                cart = (List<Item>)Session["cart"];
                int index = isExist(id);
                if (index != -1)
                {
                    cart[index].Quantity++;
                }
                else
                {
                    cart.Add(new Item { Product = _cliente.Listar_Productos_carrito(id), Quantity = 1,img = imgDataURL  });
                }
                Session["cart"] = cart;
            }

            

            return RedirectToAction("Index");
        }

        public ActionResult Remove(Int64 id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            int index = isExist(id);
            cart.RemoveAt(index);
            Session["cart"] = cart;
            return RedirectToAction("Index");
        }

        private int isExist(Int64 id)
        {
            List<Item> cart = (List<Item>)Session["cart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Product.id.Equals(id))
                    return i;
            return -1;
        }

    }
}
