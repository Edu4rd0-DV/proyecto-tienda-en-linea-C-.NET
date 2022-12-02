using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WebApplication.ServiceReference;
using WebApplication.Models;
using PagedList.Mvc;
using PagedList;

namespace WebApplication.Controllers
{
    public class InventarioController : Controller
    {
        Service1Client _cliente = new Service1Client();
        Inventariob _inv = new Inventariob();
           List <inventario_p> lista = new List<inventario_p>();
        // GET: Inventario
         

        public ActionResult Index(int? page, int? pageSize, Int64 id = 0)
           {

            if (id == 0) {
                Session["Id"] = 0;
                Session["correo"] = "Iniciar Session";
                Session["nombre"] = "";
                Session["apellido"] = "";

            }

            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;


            //defaSize, muestra el numero de resultados por defecto que se paginaran,
            //A travez de pageSize el cual contiene este valor  
            int defaSize = (pageSize ?? 9);

            ViewBag.psize = defaSize;

            //Aquí se crea la lista que se posterior mente se cargará en la vista en
            //un dropdown por medio del ViewBag.PageSize 
            ViewBag.PageSize = new List<SelectListItem>()
            {
                new SelectListItem() { Value="6", Text= "Mostrar 5" },
                new SelectListItem() { Value="10", Text= "Mostrar 10" },
                new SelectListItem() { Value="15", Text= "Mostrar 15" },
                new SelectListItem() { Value="25", Text= "Mostrar 25" },
                new SelectListItem() { Value="50", Text= "Mostrar 50" },
                new SelectListItem() { Value="10000", Text="Mostrar todos" },
            };

            Usuario _c = new Usuario();
            List <inventario_p> lista = new List<inventario_p>();
            var consulta2 = _cliente.mostrar_inventario();
            foreach (var item in consulta2)
            {
                inventario_p _inv = new inventario_p();
                _inv.id = item.id;
                _inv.id_producto = item.id_producto;
                _inv.Cantidad = item.Cantidad;
                _inv.Precio = item.Precio;
                _inv.id_proveedor = item.id_proveedor;
                string imreBase64Data = Convert.ToBase64String(item.imagen);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                _inv.imagen = imgDataURL;
                _inv.DataProducto = item.DataProducto;
                _inv.DataProveedor = item.DataProveedor;
                _inv.Datasexo = item.Datasexo;
                lista.Add(_inv);
            }
            var consulta = _cliente.mostrar_usuario_id(id);
            foreach (var item in consulta)
            {
                _c.Id = item.Id;
                _c.nombre = item.nombre;
                _c.apellido = item.apellido;
                _c.correo = item.correo;
                string imreBase64Data = Convert.ToBase64String(item.imagen);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                ViewBag.ImageData = imgDataURL;
                _c.sexo = item.sexo;
            }



            return View(lista.ToPagedList(pageIndex, defaSize));


        }

        // GET: Inventario/Details/5
        public ActionResult Details(Int64 id , Int64 id2)
        {
            if (id == 0)
            {
                Session["Id"] = 0;
                Session["correo"] = "Iniciar Session";
                Session["nombre"] = "";
                Session["apellido"] = "";
            }


            inventario_p _in = new inventario_p();
            Usuario _c = new Usuario();
            var consulta = _cliente.mostrar_inventartio_id(id);
            foreach (var item in consulta)
            {
                _in.id = item.id;
                _in.id_producto = item.id_producto;
                _in.Cantidad = item.Cantidad;
                _in.Precio = item.Precio;
                _in.id_proveedor = item.id_proveedor;
                string imreBase64Data = Convert.ToBase64String(item.imagen);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                ViewBag.ImageData2 = imgDataURL;
                _in.descripcion = item.descripcion;
                _in.DataProducto = item.DataProducto;
                _in.DataProveedor = item.DataProveedor;
                _in.Datasexo = item.Datasexo;
             
            }
            var consulta2 = _cliente.mostrar_usuario_id(id2);
            foreach (var item in consulta2)
            {
                _c.Id = item.Id;
                _c.nombre = item.nombre;
                _c.apellido = item.apellido;
                _c.correo = item.correo;
                string imreBase64Data = Convert.ToBase64String(item.imagen);
                string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                ViewBag.ImageData = imgDataURL;
                _c.sexo = item.sexo;
            }
            return View(_in);
        }

        // GET: Inventario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Inventario/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventario/Edit/5
        public ActionResult Edit(Int64 id)
        {
           
            return View();
        }

        // POST: Inventario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Inventario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Inventario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
