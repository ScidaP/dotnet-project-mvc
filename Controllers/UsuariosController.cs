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
        return View(new AgregarUsuarioViewModel());
    }

    public IActionResult UsuarioAgregado(AgregarUsuarioViewModel VM) {
        var nuevoUsuario = mapper.Map<Usuario>(VM);
        repoUsuarios.AgregarUsuario(nuevoUsuario);
        TempData["Info"] = "Usuario " + nuevoUsuario.Nombre + " agregado con éxito.";
        return RedirectToAction("Info");
    }

    public IActionResult ListarUsuarios() {
        var todosUsuarios = repoUsuarios.GetTodosUsuarios();
        return View(new ListarUsuariosViewModel(todosUsuarios));
    }

    public IActionResult Info() {
        return View();
    }
}