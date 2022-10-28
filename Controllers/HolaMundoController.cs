using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Tp4MvcNuevo.Controllers
{
    public class HolaMundoController : Controller
    {
        // 
        // GET: /HelloWorld/
        
        public IActionResult Index()
        {
            return View();
        }

        // 
        // GET: /HelloWorld/Welcome/ 
        public IActionResult Welcome()
        {
            return View();
        }
        public IActionResult ManejarDatos()
        {
            return View();
        }
        [HttpPost]
        public IActionResult RecibirDatos(string testing) {
            return View();
        }
    }
}