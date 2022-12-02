using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.ServiceReference;

namespace WebApplication.Controllers
{
    public class OtrosController : Controller
    {
        // GET: Otros
        Service1Client _cliente = new Service1Client();
        public ActionResult Index( Int64 id = 0 )
        {
            if (id == 0)
            {
                Session["Id"] = 0;
                Session["correo"] = "Iniciar Session";
                Session["nombre"] = "";
                Session["apellido"] = "";
            }

            Usuario _c = new Usuario();
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

            return View(_c);
        }

        // GET: Otros/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Otros/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Otros/Create
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

        // GET: Otros/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Otros/Edit/5
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

        // GET: Otros/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Otros/Delete/5
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
