using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Tp4MvcNuevo.Models;
using System.Text;
using Tp4MvcNuevo.ViewModels;
using AutoMapper;

namespace Tp4MvcNuevo.Controllers;

public class CadeteriasController : Controller {
    private readonly IRepositorioCadeterias repoCadeterias;
    private readonly IMapper mapper;

    public CadeteriasController(IMapper map, IRepositorioCadeterias repo) {
        repoCadeterias = repo;
        mapper = map;
    }

    public IActionResult CargarCadeteria() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                return View();
            } else {
                return RedirectToAction("Index", "Home");
            }
        }
    }

    [HttpPost]

    public IActionResult CadeteriaAgregada(Cadeteria cad) {
        repoCadeterias.AgregarCadeteria(cad);
        TempData["Info"] = "Cadetería " + cad.Nombre + " agregada con éxito.";
        return RedirectToAction("Info");
    }

    public IActionResult ListarCadeterias() {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            List<Cadeteria> ListaCadeterias = repoCadeterias.GetTodasCadeterias();
            return View(new ListarCadeteriasViewModel(ListaCadeterias));
        }
    }

    public IActionResult EliminarCadeteria(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            if (Rol == 1) {
                repoCadeterias.EliminarCadeteria(id);
                TempData["Info"] = "Cadeteria N° " + id + " eliminada con éxito";
                return RedirectToAction("Info");
            } else {
                return RedirectToAction("Index", "Home");
            }
        }
    }

    [HttpGet]
    public IActionResult MostrarCadeteria(int id) {
        int? Rol = HttpContext.Session.GetInt32("Rol");
        if (Rol == null) {
            return RedirectToAction("IniciarSesion", "Logueo");
        } else {
            Cadeteria cad = repoCadeterias.GetCadeteria(id);
            MostrarCadeteriaViewModel cadVM = mapper.Map<MostrarCadeteriaViewModel>(cad);
            return View(cadVM);
        }
    }

    public IActionResult Info() {
        return View();
    }
}