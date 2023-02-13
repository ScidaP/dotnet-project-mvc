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
    private readonly ILogger<UsuariosController> logger;

    public UsuariosController(IMapper map, IRepositorioUsuarios repo, ILogger<UsuariosController> log) {
        repoUsuarios = repo;
        mapper = map;
        logger = log;
    }

    public IActionResult AgregarUsuario() {
        var rol = HttpContext.Session.GetInt32("Rol");
        if (rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (rol == 1) {
                return View(new AgregarUsuarioViewModel());
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    [HttpPost]
    public IActionResult UsuarioAgregado(AgregarUsuarioViewModel VM) {
        var rol = HttpContext.Session.GetInt32("Rol");
        if (rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (rol == 1) {
                if (ModelState.IsValid) {
                    try {
                        var nuevoUsuario = mapper.Map<Usuario>(VM);
                        var mensaje = "Usuario " + nuevoUsuario.Nombre + " agregado con éxito.";
                        repoUsuarios.AgregarUsuario(nuevoUsuario);
                        TempData["Info"] = mensaje;
                        logger.LogInformation(mensaje);
                        return RedirectToAction("Info");
                    } catch (Exception e) {
                        logger.LogError("Error desconocido. -> " + e.ToString());
                        TempData["Info"] = "Error desconocido. Intente nuevamente. -> + " + e.Message;
                        return RedirectToAction("Info");
                    }
                } else {
                    return View("AgregarUsuario", VM);
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    public IActionResult ListarUsuarios() {
        var rol = HttpContext.Session.GetInt32("Rol");
        if (rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (rol == 1) {
                try {
                    var todosUsuarios = repoUsuarios.GetTodosUsuarios();
                    return View(new ListarUsuariosViewModel(todosUsuarios));
                } catch (Exception e) {
                    logger.LogError("Error desconocido. -> " + e.ToString());
                    TempData["Info"] = "Error desconocido. Intente nuevamente. -> + " + e.Message;
                    return RedirectToAction("Info");
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }
    [HttpGet]
    public IActionResult EliminarUsuario(int id) {
        var rol = HttpContext.Session.GetInt32("Rol");
        if (rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (rol == 1) {
                repoUsuarios.EliminarUsuario(id);
                TempData["Info"] = "Usuario " + id + " eliminado con éxito.";
                return View("Info");
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }
    [HttpGet]

    public IActionResult ActualizarUsuario(int id) {
        var rol = HttpContext.Session.GetInt32("Rol");
        if (rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (rol == 1) {
                var usuario = repoUsuarios.GetUsuario(id);
                var ActualizarUsuarioVM = mapper.Map<ActualizarUsuarioViewModel>(usuario);
                return View(ActualizarUsuarioVM);
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    public IActionResult UsuarioActualizado(ActualizarUsuarioViewModel VM) {
        var rol = HttpContext.Session.GetInt32("Rol");
        if (rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (rol == 1) {
                if (ModelState.IsValid) {
                    var usuario = mapper.Map<Usuario>(VM);
                    repoUsuarios.ActualizarUsuario(usuario);
                    var mensaje = "Usuario " + usuario.Usuario1 + " actualizado con éxito.";
                    TempData["Info"] = mensaje;
                    logger.LogInformation(mensaje);
                    return View("Info");
                } else {
                    return View("ActualizarUsuario", VM);
                }
            } else {
                return RedirectToAction("ErrorPermiso", "Home");
            }
        }
    }

    public IActionResult Info() {
        return View();
    }
}