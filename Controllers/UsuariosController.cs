using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;
using System.Text;
using Tp4MvcNuevo.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace Tp4MvcNuevo.Controllers;

public class UsuariosController : Controller {
    private readonly IRepositorioUsuarios repoUsuarios;
    private readonly IMapper mapper;
    private readonly ILogger<LogueoController> logger;

    public UsuariosController(IMapper map, IRepositorioUsuarios repo, ILogger<LogueoController> log) {
        repoUsuarios = repo;
        mapper = map;
        logger = log;
    }

    public IActionResult AgregarUsuario() {
        if (HttpContext.Session.GetInt32("Rol") == 1) {
            return View(new AgregarUsuarioViewModel());
        } else {
            return RedirectToAction("ErrorPermiso", "Home");
        }
    }

    [HttpPost]
    public IActionResult UsuarioAgregado(AgregarUsuarioViewModel VM) {
        if (HttpContext.Session.GetInt32("Rol") == 1) {
            if (ModelState.IsValid) {
                var nuevoUsuario = mapper.Map<Usuario>(VM);
                repoUsuarios.AgregarUsuario(nuevoUsuario);
                TempData["Info"] = "Usuario " + nuevoUsuario.Nombre + " agregado con éxito.";
                return RedirectToAction("Info");
            } else {
                return View("AgregarUsuario", VM);
            }
        } else {
            return RedirectToAction("ErrorPermiso", "Home");
        }
    }

    public IActionResult ListarUsuarios() {
        if (HttpContext.Session.GetInt32("Rol") == 1) {
            var todosUsuarios = repoUsuarios.GetTodosUsuarios();
            return View(new ListarUsuariosViewModel(todosUsuarios));
        } else {
            return RedirectToAction("ErrorPermiso", "Home");
        }
    }

    public IActionResult EliminarUsuario(int id) {
        if (HttpContext.Session.GetInt32("Rol") == 1) {
            repoUsuarios.EliminarUsuario(id);
            TempData["Info"] = "Usuario " + id + " eliminado con éxito.";
            return View("Info");
        } else {
            return RedirectToAction("ErrorPermiso", "Home");
        }
    }

    public IActionResult Info() {
        return View();
    }
}