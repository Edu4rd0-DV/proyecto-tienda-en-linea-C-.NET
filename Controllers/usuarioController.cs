using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using WebApplication.ServiceReference;

namespace WebApplication.Controllers
{
    public class usuarioController : Controller
    {
        Service1Client _clientte = new Service1Client();
        byte[] img;
        byte[] im;
        // GET: usuario

        public ActionResult logout()
        {
            try
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                var mensaje = ex.Message;
                ViewBag.mensaje = mensaje;
                return View("Login");
                throw;
            }
        }


        public ActionResult Index()
        {
            return View();
        }

        // GET: usuario/Details/5
        public ActionResult Details(int id)
        {
         
            return View();

        }

        // GET: usuario/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: usuario/Create
        [HttpPost]
        public ActionResult Create(Usuario p  )
        {
           HttpPostedFileBase FileBase = Request.Files[0];

            WebImage image = new WebImage(FileBase.InputStream);

             byte[] im = image.GetBytes();

            try
            {
                // TODO: Add insert logic here
                Usuario _u = new Usuario();
                _u.nombre = p.nombre;
                _u.apellido = p.apellido;
                _u.sexo = p.sexo;
                _u.correo = p.correo;
                _u.contraseña = p.contraseña;
                _u.imagen = p.imagen = im ;
                int resultado = _clientte.agregar_usuario(p);

                if (resultado > 0)
                {
                    return RedirectToAction("Login", "usuario");
                }

                return View(_u);
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message;
                return View(Mensaje);
            }
        }

        // GET: usuario/Edit/5
        public ActionResult Edit(Int64 id)
        {
            Usuario _c = new Usuario();
            var consulta = _clientte.mostrar_usuario_id(id);
            foreach (var item in consulta)
            {
                     img = item.imagen;
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

        // POST: usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(Usuario pUsuario)
        {
            HttpPostedFileBase FileBase = Request.Files[0];

                WebImage image = new WebImage(FileBase.InputStream);
                im = image.GetBytes();

         try
            {

                Usuario _u = new Usuario();
                    _u.Id = pUsuario.Id;
                    _u.nombre = pUsuario.nombre;
                    _u.apellido = pUsuario.apellido;
                    _u.sexo = pUsuario.sexo;
                    _u.correo = pUsuario.correo;
                    _u.imagen = pUsuario.imagen = im;
            
              
                    

                int resultado = _clientte.actualizar_usuario(_u);

                if (resultado > 0)
                {
                    return RedirectToAction("Login", "usuario");
                  
                }

                return View(_u);


            }
            catch
            {
                return View();
            }
        }

        // GET: usuario/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: usuario/Delete/5
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
            //------------------------------------------------------------------------------------
        [AllowAnonymous]
        // GET: Usuario/Login
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            FormsAuthentication.SignOut();
            return View();
        }

        // POST: Usuario/Login
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Usuario pUsuario)
        {
            try
            {
                // TODO: Add delete logic here
                Usuario _u = new Usuario();
                Usuario _b = new Usuario();
                _b.correo = pUsuario.correo;
                _u.correo = pUsuario.correo;
                _u.contraseña = pUsuario.contraseña;
                if (_clientte.validar_usuario(_u) == 1)
                {
                    var consulta = _clientte.mostrar_usuario_correo(_b);
                    foreach (var item in consulta)
                    {
                        Session["id"] = item.Id;
                        Session["nombre"] = item.nombre;
                        Session["apellido"] = item.apellido;
                        Session["correo"] = item.correo;
                        string imreBase64Data = Convert.ToBase64String(item.imagen);
                        string imgDataURL = string.Format("data:image/png;base64,{0}", imreBase64Data);
                        ViewBag.ImageDataL = imgDataURL;
                    }
                    return RedirectToAction("Index", "Otros", new { id = @Session["Id"].ToString() });
                }
                else
                {
                    ViewBag.Mensaje = "Error! Datos Incorrectos.";
                }
                return View(_u);
            }
            catch (Exception ex)
            {
                string Mensaje = ex.Message;
                return View(Mensaje);
            }
        }


        //----------------------------------------------------------------------------------------------------------
    

    }
    
}
