using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;
using System.Text;
using Tp4MvcNuevo.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace Tp4MvcNuevo.Controllers;

public class LogueoController : Controller {
    private readonly IRepositorioUsuarios repoUsuarios;
    private readonly IMapper mapper;

    public LogueoController(IMapper map, IRepositorioUsuarios repo) {
        repoUsuarios = repo;
        mapper = map;
    }

    public IActionResult IniciarSesion() {
        return View();
    }

    [HttpPost]
    public IActionResult SesionIniciada(IniciarSesionViewModel IniciarSesionVM) { // Funciona correctamente
        Usuario UsuarioNuevo = mapper.Map<Usuario>(IniciarSesionVM);
        int logueo = repoUsuarios.DatosCorrectos(UsuarioNuevo.Usuario1, UsuarioNuevo.Pass); // La vble "logueo" guarda -1 (si no encontró el usuario) o el id del usuario que haya encontrado.
        if (logueo > 0) {
            Usuario UsuarioLogueado = repoUsuarios.GetUsuario(logueo);
            HttpContext.Session.SetInt32("Rol", UsuarioLogueado.Rol);
            HttpContext.Session.SetString("Usuario", UsuarioLogueado.Usuario1);
            return RedirectToAction("Index", "Home");
        } else {
            return RedirectToAction("IniciarSesion", "Logueo");
        }
    }
}